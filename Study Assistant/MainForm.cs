using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Study_Assistant
{
    public partial class MainForm : Form
    {
        //Voice
        SpeechRecognitionEngine SEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer Zara;
        //Email
        public static List<string> mlist = new List<string>();
        public static List<string> mlink = new List<string>();
        public static List<string> mtitle = new List<string>();
        public static List<string> mauthor = new List<string>();
        public static List<string> msummary = new List<string>();
        public static List<string> classes = new List<string>();
        string mailusername = "minhmala00@gmail.com";
        string mailpassword = "Minhmala0!23";
        int mailindex = -1;
        int mailtrace = 0;
        string[] files, paths;
        public static String mevent;
        private FormMenu formMenuu;
        public MainForm()
        {
            InitializeComponent();

            try
            {
                Zara = new SpeechSynthesizer();
                Zara.SelectVoiceByHints(VoiceGender.Female);
                SEngine = language("en-US");
                //event handler khi nhan dc text
                SEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(MainEvent);
                //load commands
                LoadCommand();
                //use default mic
                SEngine.SetInputToDefaultAudioDevice();
                //Zara listen
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadCommand()
        {
            try
            {
                string connect = ConfigurationManager.ConnectionStrings["MyDBase"].ConnectionString;
                SqlConnection con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmand = new SqlCommand();
                cmand.Connection = con;
                cmand.CommandText = "SELECT * FROM CTable";
                SqlDataReader read = cmand.ExecuteReader();//truyen cai lenh sql vao day r execute
                while (read.Read())
                {
                    var loadcmd = read["Commands"].ToString();//lay tu cai cot commands
                    Grammar cmdgrammar = new Grammar(new GrammarBuilder(loadcmd));//=> grammar
                    SEngine.LoadGrammarAsync(cmdgrammar);
                }
                read.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Zara.SpeakAsync("Invalid Commands");
            }
            SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBase"].ConnectionString);
            scon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = scon;
            cmd.CommandText = "SELECT * FROM CTable";
            SqlDataReader reader = cmd.ExecuteReader();//truyen cai lenh sql vao day r execute
            while (reader.Read())
            {
                var loadcmd = reader["Commands"].ToString();//lay tu cai cot commands
                Grammar cmdgrammar = new Grammar(new GrammarBuilder(loadcmd));//=> grammar
                SEngine.LoadGrammarAsync(cmdgrammar);
            }
            reader.Close();
            scon.Close();


        }
        private void MainEvent(object sender, SpeechRecognizedEventArgs e)
        {
            //convert tu digital text sang text luc no nghe ra
            string commands = e.Result.Text;
            //lay ten PC ng dung
            if (commands == "hello")
            {
                Zara.SpeakAsync("Oh hi, what can i do for you!");
            }
            else if (commands == "time")
            {
                string time = DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                Zara.SpeakAsync(time);
            }
            else if (commands == "date")
            {
                string date = System.DateTime.Now.ToString("dddd") + System.DateTime.Now.ToString("dd MMMM");
                Zara.SpeakAsync(date);
            }
            else if (commands == "information")
            {
                Zara.SpeakAsync("I am Zara, your studying assistant");
            }
            else if (commands == "what can you do")
            {
                Zara.SpeakAsync("I can weather report, search web and read mails for you");
            }
            else if (commands == "get mails")
            {
                emailbtn.PerformClick();
                getemail();
                button3.PerformClick();
            }
            else if (commands == "check mails")
            {
                emailbtn.PerformClick();
                mevent = "Checkfornewmails";
                Zara.SpeakAsyncCancelAll();
                checkemail();
            }
            else if (commands == "read mails")
            {
                Zara.SpeakAsyncCancelAll();
                try
                {
                    Zara.SpeakAsync(mlist[mailtrace]);

                }
                catch
                {
                    Zara.SpeakAsync("Error");
                }
            }
            else if (commands == "next mail")
            {
                Zara.SpeakAsyncCancelAll();
                if (mailindex == -1)
                {
                    Zara.SpeakAsync("You need to use checkmail first");
                }
                else if (mailtrace == mailindex)
                {
                    Zara.SpeakAsync("there are no next mails");
                }
                else
                {
                    button2.PerformClick();
                    Zara.SpeakAsync(mlist[mailtrace]);
                }
            }
            else if (commands == "previous mail")
            {
                Zara.SpeakAsyncCancelAll();
                if (mailindex == -1)
                {
                    Zara.SpeakAsync("You need to use checkmail first");
                }
                else if (mailtrace == 0)
                {
                    Zara.SpeakAsync("there are no previous mails");
                }
                else
                {
                    button1.PerformClick();
                    Zara.SpeakAsync(mlist[mailtrace]);
                }
            }
            //// Text reader
            else if (commands == "Start Reading")
            {
                ReadBtn.PerformClick();
            }
            else if (commands == "Pause" || commands == "Resume")
            {
                PauseBtn.PerformClick();
            }
            else if (commands == "Stop")
            {
                StopBtn.PerformClick();
            }
            else if (commands == "Open File")
            {
                StopBtn.PerformClick();
                Open_fileBtn.PerformClick();
            }
            /// Video
            else if (commands == "Select video")
            {
                Zara.Speak("Choose your a video or videos that you want to watch");
                AddMusic.PerformClick();
            }
            else if (commands == "Play video")
            {
                if (VideoBox.SelectedIndex < 0)
                {
                    Zara.Speak("Oh,You haven't select the video, please select at least one");
                }
                else
                {
                    PlayBtn.PerformClick();
                }
            }
            else if (commands == "Pause video" || commands == "Continue watching")
            {
                PauseVidBtn.PerformClick();
            }
            else if (commands == "Next video")
            {
                ForwardBtn.PerformClick();
            }
            else if (commands == "Previous video")
            {
                BackwardBtn.PerformClick();
            }
            else if (commands == "Volume Up")
            {
                VolumeUpBtn.PerformClick();
            }
            else if (commands == "Volume Down")
            {
                VolumeDownBtn.PerformClick();
            }
            else if (commands == "FullScreen mode")
            {
                FullScreen.PerformClick();
            }
            else if (commands == "Exit")
            {
                MediaPlayer.Focus();
                MediaPlayer.fullScreen = false;
            }
            //news
            else if (commands == "get news")
            {
                newsbtn.PerformClick();
            }
            //timetable
            else if(commands == "Today Class")
            {
                getTimeble();
                int d = (int)System.DateTime.Now.DayOfWeek;
                char revert = '0';
                bool haveclass = false;
                switch (d)
                {
                    case 0:
                        revert = '8';
                        break;
                    case 1:
                        revert = '2';
                        break;
                    case 2:
                        revert = '3';
                        break;
                    case 3:
                        revert = '4';
                        break;
                    case 4:
                        revert = '5';
                        break;
                    case 5:
                        revert = '6';
                        break;
                    case 6:
                        revert = '7';
                        break;
                    default:
                        break;

                }
                for (var i = 0; i < classes.Count; i++)
                {
                    if (classes[i][0] == revert)
                    {
                        haveclass = true;
                        string speak = classes[i].Substring(1);
                        Zara.SpeakAsync(speak);
                    }
                }
                if (haveclass == false)
                {
                    Zara.SpeakAsync("You have no classes today");
                }
            }
            /*
            switch (commands)
            {
                case "hello":
                    
                    break;
                case "time":
                    string time = DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    Zara.SpeakAsync(time);
                    break;
                case "date":
                    string date = System.DateTime.Now.ToString("dddd") + System.DateTime.Now.ToString("dd MMMM");
                    Zara.SpeakAsync(date);
                    break;
                case "information":
                    Zara.SpeakAsync("I am Zara, your studying assistant");
                    break;
                case "what can you do":
                    Zara.SpeakAsync("I can weather report, search web and read mails for you");
                    break;
            }
            */
        }

        private SpeechRecognitionEngine language(string l)
        {
            //loop qua tung cai reginfo trong he thong nhung ngon ngu da cai xem co cai nao trung voi cai minh setup k
            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (config.Culture.ToString() == l)
                {
                    SEngine = new SpeechRecognitionEngine(config);
                    break;
                }
            }
            //k tim thay => load english
            if (SEngine == null)
            {
                MessageBox.Show("Language Not Found");
                string m = "en-US";
                foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
                {
                    if (config.Culture.ToString() == m)
                    {
                        SEngine = new SpeechRecognitionEngine(config);
                        break;
                    }
                }

            }
            return SEngine;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)//RightMenu
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LeftMenu.Width = 80;
            RightMenu.Width = 260;
            appname.Text = "SA";
            LiveResponse.SelectionIndent += 20;
            MediaPlayer.uiMode = "none";
            //button6.ForeColor = Color.Linen;
        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LMenu_Click(object sender, EventArgs e)
        {
            if (LeftMenu.Width == 80)
            {
                LeftMenu.Width = 260;
                appname.Text = "Study Assistant";
            }
            else
            {
                LeftMenu.Width = 80;
                appname.Text = "SA";
            }
        }

        private void LeftMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RightMenuButton_Click(object sender, EventArgs e)
        {
            //if (RightMenu.Width == 40)
            // {
            // RightMenu.Width = 260;
            //}
            //else
            // {
            //   RightMenu.Width = 40;
            // }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkemail();
            getemail();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void MainMenuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void LogoPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Zara.SpeakAsyncCancelAll();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void getemail()
        {
            try
            {
                WebClient client = new WebClient();
                string title, summary, response;
                XmlDocument xdoc = new XmlDocument();
                //login email
                client.Credentials = new System.Net.NetworkCredential(mailusername, mailpassword);//usename password lay tu database
                //reading data
                response = Encoding.UTF8.GetString(client.DownloadData(@"https://mail.google.com/mail/feed/atom"));//vo thu cai nay de biet xml nhin ntn
                response = response.Replace(@"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">", @"<feed>");
                //load sang xml de lay dc data de hon
                xdoc.LoadXml(response);
                string totalmail = xdoc.SelectSingleNode(@"/feed/fullcount").InnerText;
                label7.Text = totalmail; //load len form
                label4.Text = "1";
                //title + summary
                foreach (XmlNode node in xdoc.SelectNodes(@"/feed/entry"))
                {
                    textBox1.Text = node.SelectSingleNode("author").SelectSingleNode("name").InnerText;
                    if (button5.Enabled == false)
                    {
                        Zara.SpeakAsync("Email is from: " + textBox1.Text.ToString());
                    }
                    title = node.SelectSingleNode("title").InnerText;
                    textBox2.Text = title;
                    if (button5.Enabled == false)
                    {
                        Zara.SpeakAsync("The mail is about " + title.ToString());
                    }
                    summary = node.SelectSingleNode("summary").InnerText;
                    richTextBox1.Text = summary.ToString();
                }
            }
            catch (Exception ex)
            {
                Zara.SpeakAsync("Please log in your gmail on your browser");
                MessageBox.Show("Please log in your gmail on your browser or turn on the less secure app access");
                System.Diagnostics.Process.Start("https://support.google.com/accounts/answer/6010255?hl=en");
            }
        }
        private void checkemail()
        {
            if (mailindex != -1)
            {
                int a = mailindex + 1;
                Zara.SpeakAsync("You have " + a + " new emails");
                return;
            }
            string gmailurl = "https://mail.google.com/mail/feed/atom";
            XmlUrlResolver xmlResolver = new XmlUrlResolver();
            xmlResolver.Credentials = new NetworkCredential(mailusername, mailpassword);
            XmlTextReader xmlReader = new XmlTextReader(gmailurl);
            xmlReader.XmlResolver = xmlResolver;
            try
            {
                XNamespace xns = XNamespace.Get("http://purl.org/atom/ns#");
                XDocument xmlFeed = XDocument.Load(xmlReader);

                var emailinfo = from item in xmlFeed.Descendants(xns + "entry")
                                select new
                                {
                                    Author = item.Element(xns + "author").Element(xns + "name").Value,
                                    Title = item.Element(xns + "title").Value,
                                    Link = item.Element(xns + "link").Attribute("href").Value,
                                    Summary = item.Element(xns + "summary").Value
                                };
                MainForm.mlist.Clear();
                MainForm.mlink.Clear();
                foreach (var item in emailinfo)
                {
                    mailindex++;
                    MainForm.mauthor.Add(item.Author);
                    MainForm.mtitle.Add(item.Title);
                    MainForm.msummary.Add(item.Summary);
                    if (item.Title == String.Empty)
                    {
                        MainForm.mlist.Add("Mail from: " + item.Author + " have no title, Summary mail: " + item.Summary);
                        MainForm.mlink.Add(item.Link);
                    }
                    else
                    {
                        MainForm.mlist.Add("Mail from " + item.Author + ". Title: " + item.Title);
                        MainForm.mlink.Add(item.Link);
                    }
                }
                if (emailinfo.Count() > 0)
                {
                    Zara.SpeakAsync("You have " + emailinfo.Count() + " new emails");

                }
                else if (MainForm.mevent == "Checkfornewmails" && emailinfo.Count() == 0)
                {
                    Zara.SpeakAsync("You have no new mails");
                    MainForm.mevent = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Zara.SpeakAsync("Invalid Login Information");

            }



        }

        private void Email_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (mailindex == -1)
            {
                label7.Text = (0).ToString();
                return;
            }
            if (mailtrace == mailindex)
            {
                return;
            }
            mailtrace++;
            textBox1.Text = mauthor[mailtrace];
            textBox2.Text = mtitle[mailtrace];
            richTextBox1.Text = msummary[mailtrace];
            label7.Text = (mailindex + 1).ToString();
            label4.Text = (mailtrace + 1).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mailindex == -1)
            {
                label7.Text = (0).ToString();
                return;
            }
            textBox1.Text = mauthor[mailindex];
            textBox2.Text = mtitle[mailindex];
            richTextBox1.Text = msummary[mailindex];
            label7.Text = (mailindex + 1).ToString();
            label4.Text = label7.Text;
            mailtrace = mailindex;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (mailindex == -1)
            {
                label7.Text = (0).ToString();
                return;
            }
            textBox1.Text = mauthor[0];
            textBox2.Text = mtitle[0];
            richTextBox1.Text = msummary[0];
            label7.Text = (mailindex + 1).ToString();
            label4.Text = (1).ToString();
            mailtrace = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (mailindex == -1)
            {
                label7.Text = (0).ToString(); ;
                return;
            }
            if (mailtrace == 0)
            {
                return;
            }
            mailtrace--;
            textBox1.Text = mauthor[mailtrace];
            textBox2.Text = mtitle[mailtrace];
            richTextBox1.Text = msummary[mailtrace];
            label7.Text = (mailindex + 1).ToString();
            label4.Text = (mailtrace + 1).ToString();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Zara = new SpeechSynthesizer();
                Zara.SelectVoiceByHints(VoiceGender.Female);
                SEngine = language("en-US");
                //event handler khi nhan dc text
                SEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(MainEvent);
                //load commands
                LoadCommand();
                //use default mic
                SEngine.SetInputToDefaultAudioDevice();
                //Zara listen
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SEngine.RecognizeAsync(RecognizeMode.Multiple);
            button6.Enabled = true;
            button5.Enabled = false;
            phead.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Zara.SpeakAsyncCancelAll();
            SEngine.RecognizeAsyncStop();
            button6.Enabled = false;
            button5.Enabled = true;
            phead.Enabled = false;
        }
        private void button7_Click_3(object sender, EventArgs e)
        {
            if (Zara.State == SynthesizerState.Speaking)
            {
                Zara.Pause();
                phead.Text = "RESUME";
            }
            else
            {
                if (Zara.State == SynthesizerState.Paused)
                {
                    Zara.Resume();
                    phead.Text = "PAUSE";
                }
            }
        }
        private void button7_Click_1(object sender, EventArgs e)// Open button in Audio text
        {
            //StopBtn.PerformClick();
            PauseBtn.Enabled = true;
            Zara.SpeakAsync(ReadTxt.Text);
        }

        private void Open_fileBtn_Click(object sender, EventArgs e)
        {
            if (Zara.State == SynthesizerState.Speaking)
            {
                Zara.SpeakAsyncCancelAll();
            }
            ReadTxt.Clear();
            Zara.SpeakAsync("Choose a text file,Please");
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf|All files (*.*)|(*.*)";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strfilename = openFileDialog1.FileName;
                    string filetext = File.ReadAllText(strfilename);
                    ReadTxt.Text = filetext;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file");
                }
            }
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            if (Zara.State == SynthesizerState.Speaking)
            {
                Zara.Pause();
                PauseBtn.Text = "Resume";
            }
            else
            {
                if (Zara.State == SynthesizerState.Paused)
                {
                    Zara.Resume();
                    PauseBtn.Text = "Pause";
                }
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            ReadTxt.Clear();
            //if (Zara.State == SynthesizerState.Speaking)
            //{
            Zara.SpeakAsyncCancelAll();
            ReadBtn.Enabled = true;
            PauseBtn.Text = "Pause";
            //}
        }

        private void Video_Click(object sender, EventArgs e)
        {

        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            if (VideoBox.SelectedIndex >= 0)
            {
                MediaPlayer.URL = paths[VideoBox.SelectedIndex];
                MediaPlayer.Ctlcontrols.play();
            }
            else
            {
                VideoBox.SelectedIndex = -1;
                MessageBox.Show("Please select videos");
                //MediaPlayer.Ctlcontrols.play();
            }
        }

        private void VolumeUpBtn_Click(object sender, EventArgs e)
        {
            if (MediaPlayer.settings.volume < 100)
            {
                MediaPlayer.settings.volume += 10;
            }
        }

        private void BackwardBtn_Click(object sender, EventArgs e)
        {
            if (MediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (VideoBox.SelectedIndex > 0)
                {
                    MediaPlayer.Ctlcontrols.previous();
                    VideoBox.SelectedIndex--;
                    VideoBox.Update();
                }
                else
                {
                    VideoBox.SelectedIndex = 0;
                    VideoBox.Update();
                }
            }
        }

        private void PauseVidBtn_Click(object sender, EventArgs e)
        {
            if (MediaPlayer.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                MediaPlayer.Ctlcontrols.play();
            }
            else
            {
                MediaPlayer.Ctlcontrols.pause();
            }
        }

        private void ForwardBtn_Click(object sender, EventArgs e)
        {
            if (MediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (VideoBox.SelectedIndex < (VideoBox.Items.Count - 1))
                {
                    MediaPlayer.Ctlcontrols.next();
                    VideoBox.SelectedIndex++;
                    VideoBox.Update();
                }
                else
                {
                    VideoBox.SelectedIndex = 0;
                    VideoBox.Update();
                }
            }
        }

        private void VolumeDownBtn_Click(object sender, EventArgs e)
        {
            if (MediaPlayer.settings.volume > 0)
            {
                MediaPlayer.settings.volume -= 10;
            }
        }

        private void FullScreen_Click(object sender, EventArgs e)
        {
            if (MediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                MediaPlayer.fullScreen = true;
            }
            else
            {
                MediaPlayer.fullScreen = false;
            }
        }
        private void newsbtn_Click(object sender, EventArgs e)
        {
            WebClient webc = new WebClient();
            string page = webc.DownloadString("https://news.google.com/?edchanged=1&ned=us&authuser=0");
            TodayNews.Navigate("https://news.google.com/?edchanged=1&ned=us&authuser=0");
            TodayNews.ScriptErrorsSuppressed = true;
        }
        private void button7_Click_2(object sender, EventArgs e)
        {
            WebClient webc = new WebClient();
            string page = webc.DownloadString("http://www.bing.com/news/search?q=&nvaug=%5bNewsVertical+Category%3d%22rt_World%22%5d&FORM=NSBABR");
            TodayNews.Navigate("http://www.bing.com/news/search?q=&nvaug=%5bNewsVertical+Category%3d%22rt_World%22%5d&FORM=NSBABR");
            TodayNews.ScriptErrorsSuppressed = true;
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            WebClient webc = new WebClient();
            string page = webc.DownloadString("https://www.hcmut.edu.vn/en/newsletter/view/university-news");
            TodayNews.Navigate("https://www.hcmut.edu.vn/en/newsletter/view/university-news");
            TodayNews.ScriptErrorsSuppressed = true;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            WebClient webc = new WebClient();
            string page = webc.DownloadString("https://thanhnien.vn/");
            TodayNews.Navigate("https://thanhnien.vn/");
            TodayNews.ScriptErrorsSuppressed = true;
        }
        private void MenuFootPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void audiobtn_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        /*
        private void button7_Click_2(object sender, EventArgs e)
        {
            stopnews.PerformClick();
            playnews.Enabled = false;
            pausenews.Enabled = true;
            Zara.SpeakAsync(NewsText.Text);
        }

        private void pausenews_Click(object sender, EventArgs e)
        {
            if (Zara.State == SynthesizerState.Speaking)
            {
                Zara.Pause();
                pausenews.Text = "Resume";
            }
            else
            {
                if (Zara.State == SynthesizerState.Paused)
                {
                    Zara.Resume();
                    pausenews.Text = "Pause";
                }
            }
        }

        private void stopnews_Click(object sender, EventArgs e)
        {
            if (Zara.State == SynthesizerState.Paused)
            {
                Zara.Resume();
            }
            Zara.SpeakAsyncCancelAll();
            stopnews.Text = "Stop";
            playnews.Enabled = true;
        }
        */
        private void getnew_Click(object sender, EventArgs e)
        {
            newsbtn.PerformClick();
        }

        private void TodayNews_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void MediaPlayer_Enter(object sender, EventArgs e)
        {

        }

        private void ReadTxt_TextChanged(object sender, EventArgs e)
        {

        }
        private void WRefreshBtn_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void WBackwardBtn_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void WForwardBtn_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void WSearchBtn_Click(object sender, EventArgs e)
        {
            string UrlAddress = keywordstxt.Text;
            webBrowser1.Navigate("https://www.bing.com/search?q=" + UrlAddress);
        }
        private void Navigate(String address) //dieu huong toi web
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://www.bing.com/search?q=/") &&
                !address.StartsWith("https://www.bing.com/search?q=/"))
            {
                address = "https://www.bing.com/search?q=/" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }

        private void webtn_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void keywordstxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        string APIKey = "440a00ddf7631534cb2a256cd7db532f"; // API code take by : apiweather.org

        private void wbtn_Click(object sender, EventArgs e)
        {

        }
        // create variable lon , lat
        public double lon;
        public double lat;
        DateTime convertDateTime(long sec) // convert time
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();

            return day;
        }
        //create getWeather function
        void getWeather()
        {
            try
            {
                FLP.Controls.Clear(); //Restore
                using (WebClient web = new WebClient())
                {

                    // create string with info address web
                    string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", textSearch.Text, APIKey);
                    var json = web.DownloadString(url);
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);


                    //create variable lon and lat strore lon , lat
                    lon = Info.coord.lon;
                    lat = Info.coord.lat;



                    // icon image stored from website
                    picIcon.ImageLocation = "http://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                    //backgroundpic.ImageLocation = "https://api.pexels.com/v1/search?query=" + textSearch.Text + "&per_page=1";

                    labCondition.Text = Info.weather[0].main;
                    labDetails.Text = Info.weather[0].description;

                    //convert miliseconds to day
                    labSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                    labelSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();

                   // string a = Info.snow;
                    //convert temp kelvin to c
                    labTemp.Text = ((int)(Info.main.temp - 273)).ToString() + " °C";
                    labNation.Text = Info.sys.country.ToString();

                    //wind speed , convert m/s to km/h

                    labWindSpeed.Text = (3.6 * Info.wind.speed).ToString() + " km/h";

                    //output pressure
                    labPress.Text = Info.main.pressure.ToString() + " mb";
                    //if (Info.main.temp - 273 >= 30)
                    //{
                    //    Weather.BackColor = System.Drawing.Color.FromArgb(255, 255, 179);
                    //}
                    //else if(Info.main.temp -273 < 30 && Info.main.temp > 273)
                    //{
                    //    Weather.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
                        //Weather.BackgroundImage = Resources.backward-solid.jpg
                    //}
                    //else
                    //{
                    //    Weather.BackColor = System.Drawing.Color.FromArgb(114, 160, 193);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Location");
            }
        }

        // create getforecast function 
        void getForeCast()
        {

            using (WebClient web = new WebClient())
            {
                //create url string stored address api openweather
                string url = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude=current.minutely.hourly.daily&appid={2}", lat, lon, APIKey);
                var json = web.DownloadString(url);
                WeatherForecast.ForecastInfo ForecastInfo = JsonConvert.DeserializeObject<WeatherForecast.ForecastInfo>(json);

                ForecastUC FUC;
                // day loop 
                for (int i = 0; i < 8; i++)
                {
                    FUC = new ForecastUC();
                    //image of weather icon stored from api weather 
                    FUC.picWeatherIcon.ImageLocation = "http://openweathermap.org/img/w/" + ForecastInfo.daily[i].weather[0].icon + ".png";
                    FUC.labMainWeather.Text = ForecastInfo.daily[i].weather[0].main;

                    FUC.labWeatherDescription.Text = ForecastInfo.daily[i].weather[0].description;

                    //convert time to day
                    FUC.labDT.Text = convertDateTime(ForecastInfo.daily[i].dt).DayOfWeek.ToString();
                    FUC.labDAY.Text = convertDateTime(ForecastInfo.daily[i].dt).ToString("d");

                    //output min and max temp 
                    //corvert kelvin temp to c temp
                    FUC.labTempMin.Text = ((int)(ForecastInfo.daily[i].temp.min) - 273).ToString() + " °C";
                    FUC.labTempMax.Text = ((int)(ForecastInfo.daily[i].temp.max) - 273).ToString() + " °C";

                    //add fuc to flp object
                    FLP.Controls.Add(FUC);

                }


            }

        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            getWeather();
            getForeCast();
        }

        private void AddMusic_Click(object sender, EventArgs e)
        {
            string UserName = System.Environment.UserName;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\" + UserName + "\\Documents\\MyMusic";
            ofd.Filter = "(mp3,wav,mp4,mov,wmv,avi,3gp,flv)|*.mp3;*.mp4;*.wav;*.3gp;*.avi;*.mov;*.flv;*.wmv;*.mpg|all files|*.*";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                files = ofd.SafeFileNames;
                paths = ofd.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    VideoBox.Items.Add(files[i]);
                }
            }
        }

        public int GetWeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        private void timetabletbn_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            getTimeble();

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void NEWS_Click(object sender, EventArgs e)
        {

        }

        private void giavang_Click(object sender, EventArgs e)
        {
            WebClient webc = new WebClient();
            string page = webc.DownloadString("https://vnexpress.net/");
            TodayNews.Navigate("https://vnexpress.net/");
            TodayNews.ScriptErrorsSuppressed = true;
        }

        private void giacoin_Click(object sender, EventArgs e)
        {
            WebClient webc = new WebClient();
            string page = webc.DownloadString("https://docbao.vn/");
            TodayNews.Navigate("https://docbao.vn/");
            TodayNews.ScriptErrorsSuppressed = true;
        }

        private void giachungkhoan_Click(object sender, EventArgs e)
        {
            WebClient webc = new WebClient();
            string page = webc.DownloadString("https://tienphong.vn/");
            TodayNews.Navigate("https://tienphong.vn/");
            TodayNews.ScriptErrorsSuppressed = true;
        }

        private void baotuoitre_Click(object sender, EventArgs e)
        {
            WebClient webc = new WebClient();
            string page = webc.DownloadString("https://nhandan.vn/");
            TodayNews.Navigate("https://nhandan.vn/");
            TodayNews.ScriptErrorsSuppressed = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            label7.Text = "0";
            label4.Text = "0";

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MediaPlayer_Enter_1(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            MediaPlayer.Ctlcontrols.stop();
            VideoBox.Items.Clear();
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            
        }

        private void VideoBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Weather_Click(object sender, EventArgs e)
        {

        }

        private void labTemp_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            Application.Run(new FormMenu());
        }

        private void Title_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LivePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LiveResponse_TextChanged(object sender, EventArgs e)
        {

        }

        private void DebugTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Game_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void PlayList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Audio_Click(object sender, EventArgs e)
        {

        }

        private void FLP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labNation_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void labPress_Click(object sender, EventArgs e)
        {

        }

        private void labWindSpeed_Click(object sender, EventArgs e)
        {

        }

        private void labPressure_Click(object sender, EventArgs e)
        {

        }

        private void labWind_Click(object sender, EventArgs e)
        {

        }

        private void labelSunrise_Click(object sender, EventArgs e)
        {

        }

        private void Sunset_Click(object sender, EventArgs e)
        {

        }

        private void labSunset_Click(object sender, EventArgs e)
        {

        }

        private void Sunrise_Click(object sender, EventArgs e)
        {

        }

        private void labDetails_Click(object sender, EventArgs e)
        {

        }

        private void labCondition_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Web_Click(object sender, EventArgs e)
        {

        }

        private void labDate_Click(object sender, EventArgs e)
        {

        }

        private void labWeek_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void picTuan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picCoso_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picPhonghoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picGiohoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picTiet_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picThu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picNhom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picTinchi_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picTENMH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picMaMH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void labName_Click(object sender, EventArgs e)
        {

        }

        private void labID_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void picIcon_Click(object sender, EventArgs e)
        {

        }

        private void backgroundpic_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Playgame_Click(object sender, EventArgs e)
        {
            //this.Hide();
            formMenuu = new FormMenu();
            formMenuu.Show();
            

        }

        private void TodayClass_Click(object sender, EventArgs e)
        {
            getTimeble();
            int d = (int)System.DateTime.Now.DayOfWeek;
            char revert = '0';
            bool haveclass = false;
            switch (d)
            {
                case 0:
                    revert = '8';
                    break;
                case 1:
                    revert = '2';
                    break;
                case 2:
                    revert = '3';
                    break;
                case 3:
                    revert = '4';
                    break;
                case 4:
                    revert = '5';
                    break;
                case 5:
                    revert = '6';
                    break;
                case 6:
                    revert = '7';
                    break;
                default:
                    break;

            }
            for (var i = 0; i < classes.Count; i++)
            {
                if (classes[i][0] == revert)
                {
                    haveclass = true;
                    string speak = classes[i].Substring(1);
                    Zara.SpeakAsync(speak);
                }
            }
            if (haveclass == false)
            {
                Zara.SpeakAsync("You have no classes today");
            }
        }

        void getTimeble()
        {

            MaMH maMH = new MaMH();
            TenMH tenMH = new TenMH();
            Tinchi tinchi = new Tinchi();
            nhom Nhom = new nhom();
            thu Thu = new thu();
            tiet Tiet = new tiet();
            giohoc Giohoc = new giohoc();
            phonghoc Phonghoc = new phonghoc();
            coso Coso = new coso();
            tuanhoc Tuanhoc = new tuanhoc();
            labWeek.Text = GetWeekNumber().ToString();
            labDate.Text = DateTime.Now.ToString("D");
            for (int i = 0; i < 8; i++)
            {
                maMH = new MaMH();
                tenMH = new TenMH();
                tinchi = new Tinchi();
                Nhom = new nhom();
                Thu = new thu();
                Tiet = new tiet();
                Giohoc = new giohoc();
                Phonghoc = new phonghoc();
                Coso = new coso();
                Tuanhoc = new tuanhoc();
                string connectclass;
                switch (i)
                {
                    case 0:
                        maMH.labMMH1.Text = "CO2017";
                        tenMH.labTENMH1.Text = "Operating System";
                        tinchi.labtinchi.Text = "3";
                        Nhom.labNhom.Text = "L04";
                        Thu.labThu.Text = "3";
                        Tiet.labTiet.Text = "11-12";
                        Giohoc.labGiohoc.Text = "16:00 - 17:50";
                        Phonghoc.labPhong.Text = "HANGOUT";
                        Coso.labCoso.Text = "BK-CS1";
                        Tuanhoc.labTuanhoc.Text = "01|02|03|04|--|--|07|08|09|--|--|--|--|14|15|16|17|18|";
                        connectclass = (Thu.labThu.Text) + tenMH.labTENMH1.Text + "at 16 o' clock";
                        MainForm.classes.Add(connectclass);
                        break;
                    case 1:
                        maMH.labMMH1.Text = "SP1033";
                        tenMH.labTENMH1.Text = "Marxist Political Economy";
                        tinchi.labtinchi.Text = "2";
                        Nhom.labNhom.Text = "L10";
                        Thu.labThu.Text = "4";
                        Tiet.labTiet.Text = "2-3";
                        Giohoc.labGiohoc.Text = "7:00 - 8:50";
                        Phonghoc.labPhong.Text = "HANGOUT";
                        Coso.labCoso.Text = "BK-CS1";
                        Tuanhoc.labTuanhoc.Text = "01|02|03|04|--|--|07|08|09|--|--|--|--|14|15|16|";
                        connectclass = (Thu.labThu.Text) + tenMH.labTENMH1.Text + "at 7 o' clock in the morning";
                        MainForm.classes.Add(connectclass);
                        break;
                    case 2:
                        maMH.labMMH1.Text = "CO2014";
                        tenMH.labTENMH1.Text = "Database System (Laboratory)";
                        tinchi.labtinchi.Text = "--";
                        Nhom.labNhom.Text = "L01";
                        Thu.labThu.Text = "4";
                        Tiet.labTiet.Text = "7-11";
                        Giohoc.labGiohoc.Text = "12:00 - 16:50";
                        Phonghoc.labPhong.Text = "H6-708";
                        Coso.labCoso.Text = "BK-CS2";
                        Tuanhoc.labTuanhoc.Text = "--|--|--|04|--|--|--|08|--|--|--|--|--|14|--|16|--|18|--|20|";
                        connectclass = (Thu.labThu.Text) + tenMH.labTENMH1.Text + " at twelve o' clock";
                        MainForm.classes.Add(connectclass);
                        break;
                    case 3:
                        maMH.labMMH1.Text = "CO2039";
                        tenMH.labTENMH1.Text = "Advanced Programming";
                        tinchi.labtinchi.Text = "3";
                        Nhom.labNhom.Text = "L04";
                        Thu.labThu.Text = "5";
                        Tiet.labTiet.Text = "2-3";
                        Giohoc.labGiohoc.Text = "7:00 - 8:50";
                        Phonghoc.labPhong.Text = "HANGOUT";
                        Coso.labCoso.Text = "BK-CS1";
                        Tuanhoc.labTuanhoc.Text = "01|02|03|--|--|06|07|08|09|--|--|--|--|14|15|16|17|18|";
                        connectclass = (Thu.labThu.Text) + tenMH.labTENMH1.Text + " at 7 o' clock in the morning";
                        MainForm.classes.Add(connectclass);
                        break;
                    case 4:
                        maMH.labMMH1.Text = "IM1021";
                        tenMH.labTENMH1.Text = "Starting a business";
                        tinchi.labtinchi.Text = "3";
                        Nhom.labNhom.Text = "L03";
                        Thu.labThu.Text = "5";
                        Tiet.labTiet.Text = "7-8";
                        Giohoc.labGiohoc.Text = "12:00 - 13:50";
                        Phonghoc.labPhong.Text = "HANGOUT";
                        Coso.labCoso.Text = "BK-CS1";
                        Tuanhoc.labTuanhoc.Text = "01|02|03|--|--|06|07|08|09|--|--|--|--|14|15|16|17|18|";
                        connectclass = (Thu.labThu.Text) + tenMH.labTENMH1.Text + "at 12 o' clock";
                        MainForm.classes.Add(connectclass);
                        break;
                    case 5:
                        maMH.labMMH1.Text = "CO2013";
                        tenMH.labTENMH1.Text = "Database System";
                        tinchi.labtinchi.Text = "4";
                        Nhom.labNhom.Text = "L01";
                        Thu.labThu.Text = "6";
                        Tiet.labTiet.Text = "2-4";
                        Giohoc.labGiohoc.Text = "7:00 - 9:50";
                        Phonghoc.labPhong.Text = "HANGOUT";
                        Coso.labCoso.Text = "BK-CS1";
                        Tuanhoc.labTuanhoc.Text = "01|02|03|--|--|06|07|08|09|--|--|--|--|14|15|16|17|18|";
                        connectclass = (Thu.labThu.Text) + tenMH.labTENMH1.Text + "at 7 o' clock in the morning";
                        MainForm.classes.Add(connectclass);
                        break;
                    case 6:
                        maMH.labMMH1.Text = "SP1007";
                        tenMH.labTENMH1.Text = "General Law of Vietnam";
                        tinchi.labtinchi.Text = "2";
                        Nhom.labNhom.Text = "L04";
                        Thu.labThu.Text = "6";
                        Tiet.labTiet.Text = "7-8";
                        Giohoc.labGiohoc.Text = "12:00 - 13:50";
                        Phonghoc.labPhong.Text = "HANGOUT";
                        Coso.labCoso.Text = "BK-CS1";
                        Tuanhoc.labTuanhoc.Text = "01|02|03|--|--|06|07|08|09|--|--|--|--|14|15|16|";
                        connectclass = (Thu.labThu.Text) + tenMH.labTENMH1.Text + "at 12 o' clock";
                        MainForm.classes.Add(connectclass);
                        break;
                    case 7:
                        maMH.labMMH1.Text = "CO2018";
                        tenMH.labTENMH1.Text = "Operating System (Laboratory)";
                        tinchi.labtinchi.Text = "--";
                        Nhom.labNhom.Text = "L07";
                        Thu.labThu.Text = "8";
                        Tiet.labTiet.Text = "2-6";
                        Giohoc.labGiohoc.Text = "7:00 - 11:50";
                        Phonghoc.labPhong.Text = "H6-604";
                        Coso.labCoso.Text = "BK-CS2";
                        Tuanhoc.labTuanhoc.Text = "--|--|03|--|--|06|--|08|--|--|--|--|--|14|";
                        connectclass = (Thu.labThu.Text) + tenMH.labTENMH1.Text + "at 7 o' clock in the morning";
                        MainForm.classes.Add(connectclass);
                        break;
                }

                picTENMH.Controls.Add(tenMH);
                picMaMH.Controls.Add(maMH);
                picTinchi.Controls.Add(tinchi);
                picNhom.Controls.Add(Nhom);
                picThu.Controls.Add(Thu);
                picTiet.Controls.Add(Tiet);
                picGiohoc.Controls.Add(Giohoc);
                picPhonghoc.Controls.Add(Phonghoc);
                picCoso.Controls.Add(Coso);
                picTuan.Controls.Add(Tuanhoc);
            }



        }

    }
}
