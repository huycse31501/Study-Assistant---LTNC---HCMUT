using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Study_Assistant
{
    public partial class FormGameplay : Form
    {
        //Properties 
        public int levelCurrent;
        private LevelGame levelAnim;
        private bool gameOver = false;
        private int score = 0;
        private int delay = 0;
        private int timingControls;     //Lí do thêm : Quản lí thời gian tạo item 
        private Item item = null;       //Quản lí item đc thêm vào Map
        private Timer time = new Timer();// Do không biết chức năng của timerGame, tạo biến Timer mới
        private Player player;
        private Zombie zombie1;
        private Zombie zombie2;
        private Zombie zombie3;
        private List<Zombie> zombies = new List<Zombie>();
        private Random rd = new Random();
        private SoundPlayer music = new SoundPlayer(Properties.Resources.burst);
        private FormMenu frmMenu;
        //Initialize
        public FormGameplay()
        {

            InitializeComponent();
            time.Tick += tm_Tick;
            time.Interval = 1000;
            time.Start();
        }
        public FormGameplay(FormMenu formLogin)
        {
            InitializeComponent();
            frmMenu = formLogin;
            lbLevel.Parent = MAP;
            levelAnim = new LevelGame(lbLevel);
            levelCurrent = 1;
            time.Tick += tm_Tick;
            time.Interval = 1000;
            time.Stop();
            timerGame.Stop();
        }
        //Method
        //+Hàm tạo hình ảnh nhân vật theo lựa chọn character
        private void MakeSkin()
        {
            if (frmMenu.select == 0)
                player.skin.MakePlayer1();
            if (frmMenu.select == 1)
                player.skin.MakePlayer2();
            if (frmMenu.select == 2)
                player.skin.MakeSuperPlayer();
            if (frmMenu.select == 3)
            {
                player.skin.MakePlayer4();
            }
        }
        //Hàm khởi tạo player và zombie 
        public void InitPlayerVsZombie()
        {
            player = new Player(MAP);
            MakeSkin();
            player.MakeAvatar();
            zombie1 = new Zombie(MAP, 0, MAP.Height / 4, "down");
            zombie2 = new Zombie(MAP, MAP.Width - 100, MAP.Height / 4, "down");
            zombie3 = new Zombie(MAP, MAP.Width / 2, MAP.Height - 120, "up");
            zombie1.MakeAvatar();
            zombie2.MakeAvatar();
            zombie3.MakeAvatar();
            zombies.Add(zombie1);
            zombies.Add(zombie2);
            zombies.Add(zombie3);
            SetTransparent(MAP, player.Avatar);
            foreach (Zombie zb in zombies)
                SetTransparent(MAP, zb.Avatar);
            timingControls = 5;
        }
        //Hàm làm trong suốt nền một hình ảnh 
        private void SetTransparent(PictureBox Map, PictureBox sender)
        {
            sender.BringToFront();
            sender.BackColor = Color.Transparent;
            //bug
            sender.Parent = Map;
        }
        //Hàm bắt sự kiện nhấn phím
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
                return;
            player.NhanPhim(sender, e);
        }
        //Hàm bắt sự kiện nhả phím
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (gameOver)
                return;
            player.NhaPhim(sender, e);
        }
        private void ShowLevelGame()
        {
            int flag = 0;
            if (score == 0)
                levelCurrent = 1;
            else if (score == 10)
                levelCurrent = 2;
            else if (score == 20)
                levelCurrent = 3;
            else if (score == 50)
                levelCurrent = 4;
            else if (score == 100)
                levelCurrent = 5;
            else if (score == 150)
                levelCurrent = 6;
            else if (score == 200)
                levelCurrent = 7;
            else
                flag = 1;
            if (flag == 0 && (levelCurrent != levelAnim.level))
            {
                levelAnim.level = levelCurrent;
                levelAnim.Animation();
            }
        }
        //Hàm bắt đầu game
        private void LoadFrames(object sender, EventArgs e)
        {
            ShowLevelGame();
            delay++;
            if (player.playerHealth > 1)
            {
                hp.Value = Convert.ToInt32(player.playerHealth);
            }
            else
            {
                player.Avatar.Image = Properties.Resources.dead;
                time.Stop();
                timerGame.Stop();
                player.amthanh.Dispose();
                player.amthanh = null;
                music.Stop();
                gameOver = true;
                frmMenu.UpdateScore(score);
                CallFormLogin();
            }
            lbAmmo.Text = "Ammo: " + player.ammo.ToString();
            lbKills.Text = "Kills: " + score.ToString();
            if (player.playerHealth < 20)
            {
                hp.ForeColor = Color.Red;
            }
            player.MoveAvatar(sender, e);

            //Đã viết lại : Xử lí va chạm giữa player và các item
            foreach (Control x in MAP.Controls)
            {
                #region Hòm tiếp tế va chạm với người
                //Xử lý va chạm hòm tiếp tế và đạn ra ngoài màn hình
                if (x is PictureBox && Equals(x.Tag, "ammo"))
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Avatar.Bounds))
                    {
                        //((PictureBox)x).Dispose();

                        PictureBox t = x as PictureBox;
                        t.Dispose();
                        t = null;
                        //MAP.Controls.Remove(((PictureBox)x));
                        MAP.Controls.Remove(t);
                        AmmoItem i = (AmmoItem)item;
                        player.ammo += i.NumberAmmo;
                        i.DestroyItem();
                        item = null;
                    }
                }
                // Va chạm hộp cứu thương
                else if (x is PictureBox && Equals(x.Tag, "firstaid"))
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Avatar.Bounds))
                    {
                        PictureBox t = x as PictureBox;
                        t.Dispose();
                        t = null;
                        MAP.Controls.Remove(t);
                        FirstAidItem i = (FirstAidItem)item;
                        player.playerHealth += i.Health;
                        if (player.playerHealth > 100)
                            player.playerHealth = 100;
                        i.DestroyItem();
                        item = null;
                    }
                }
                //Va chạm item nâng cấp
                else if (x is PictureBox && Equals(x.Tag, "upgrade"))
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Avatar.Bounds))
                    {
                        PictureBox t = x as PictureBox;
                        t.Dispose();
                        t = null;
                        MAP.Controls.Remove(t);
                        UpgradeItem i = (UpgradeItem)item;
                        player.bulletLevel++;
                        i.DestroyItem();
                        item = null;
                    }
                }
                #endregion

                #region Đạn đi ra ngoài
                if (x is PictureBox && Equals(x.Tag, "bullet"))
                {
                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 795 || ((PictureBox)x).Top < 30 || ((PictureBox)x).Top > 645)
                    {
                        MAP.Controls.Remove((PictureBox)x);
                        ((PictureBox)x).Dispose();
                    }
                }
                #endregion

                //Xử lý va chạm zombie với đạn
                if (x is PictureBox && Equals(x.Tag, "zombie"))
                {
                    #region Zombie va chạm với người
                    PictureBox zombie = x as PictureBox;
                    if (x.Bounds.IntersectsWith(player.Avatar.Bounds))
                    {
                        //Khoảng 2 giây đầu người chơi sẽ không mất máu
                        if (delay > 100)
                        {
                            if (levelCurrent == 4)
                                player.playerHealth -= 2;
                            else if (levelCurrent == 5)
                                player.playerHealth -= 3;
                            else if (levelCurrent == 6)
                                player.playerHealth -= 4;
                            else if (levelCurrent == 7)
                                player.playerHealth -= 5;
                            else
                                player.playerHealth -= 1;
                        }
                        SetTransparent(zombie, player.Avatar);
                    }
                    else
                    {
                        SetTransparent(MAP, player.Avatar);
                    }
                    #endregion

                    foreach (Control y in MAP.Controls)
                    {
                        #region Zombie va chạm với đạn
                        if (y is PictureBox && Equals(y.Tag, "bullet") && (x is PictureBox && Equals(x.Tag, "zombie")))
                        {
                            if (x.Bounds.IntersectsWith(y.Bounds))
                            {
                                music.Play();
                                score++;
                                PictureBox bl = y as PictureBox;
                                PictureBox zb = x as PictureBox;
                                //bug
                                bl.Dispose();
                                bl = null;
                                MAP.Controls.Remove(bl);
                                zb.Tag = null;
                                MAP.Controls.Remove(zb);
                                AnimationEvent(x.Left, x.Top);
                                Spawner();
                                RemoveZombieList();
                            }
                        }
                        #endregion

                    }

                }
            }
            MoveZombies();
        }
        private void MakeSpeedForZombies()
        {
            int speed;
            if (levelCurrent == 3)
                speed = 2;
            else if (levelCurrent == 4 || levelCurrent == 5)
                speed = 3;
            else if (levelCurrent == 6 || levelCurrent == 7)
                speed = 4;
            else
                speed = 1;
            foreach (Zombie zb in zombies)
                zb.IncreaseSpeed(speed);
        }
        //+Thực hiện di chuyển zombies
        private void MoveZombies()
        {
            MakeSpeedForZombies();
            foreach (Zombie zb in zombies)
                zb.MoveEnemy(player);
        }
        //Hàm tạo ra animation khi zombie bị tiêu diệt
        private void AnimationEvent(int x, int y)
        {
            Animation animation = new Animation(MAP);
            animation.MakeAnimation(x, y);
        }
        //Hàm tạo và thêm một zombie vào map
        //Chinh sua lai
        private void Spawner()
        {
            int x, y;
            do
            {
                x = rd.Next(0, MAP.Width - 100);
                y = rd.Next(30, MAP.Height - 120);
            } while (IsCollision(x, y));
            string direction;
            int i = rd.Next(0, 3);
            switch (i)
            {
                case 0: direction = "left"; break;
                case 1: direction = "right"; break;
                case 2: direction = "up"; break;
                case 3: direction = "down"; break;
                default: direction = "down"; break;
            }
            Zombie zb = new Zombie(MAP, x, y, direction);
            zb.MakeAvatar();
            SetTransparent(MAP, zb.Avatar);
            zombies.Add(zb);
            RemoveZombieList();
        }
        private bool IsCollision(int x, int y)
        {
            foreach (Control item in MAP.Controls)
            {
                if (item is PictureBox && Equals(item.Tag, "zombie"))
                {
                    if (x == item.Left && y == item.Top)
                        return true;
                }
            }
            return false;
        }
        //Hàm remove danh sách zombie hiện có
        private void RemoveZombieList()
        {
            for (int i = zombies.Count - 1; i >= 0; i--)
            {
                if (zombies[i].Avatar.Tag == null)
                {
                    zombies[i].skin.imleft.Dispose();
                    zombies[i].skin.imright.Dispose();
                    zombies[i].skin.imup.Dispose();
                    zombies[i].skin.imdown.Dispose();
                    zombies[i].skin.imleft = null;
                    zombies[i].skin.imright = null;
                    zombies[i].skin.imup = null;
                    zombies[i].skin.imdown = null;
                    zombies.Remove(zombies[i]);
                }
            }
        }
        //Hàm gọi formLogin
        private void CallFormLogin()
        {
            frmMenu.ShowFormEnd();
            frmMenu.Show();
        }
        public void GameStart()
        {

            if (player != null)
            {
                player.Avatar.Dispose();
                player.Avatar = null;
                player = null;
            }
            for (int i = zombies.Count() - 1; i >= 0; i--)
                if (zombies[i] != null)
                {
                    zombies[i].Avatar.Dispose();
                    zombies[i].Avatar = null;
                    zombies[i] = null;
                }
            zombies = new List<Zombie>();
            foreach (Control item in this.Controls)
                if (item is PictureBox)
                {
                    if (Equals(item.Tag, "zombie") || Equals(item.Tag, "player") || Equals(item.Tag, "ammo"))
                        this.Controls.Remove(item);
                }
            score = 0;
            gameOver = false;
            InitPlayerVsZombie();
            time.Start();
            timerGame.Start();
        }


        //Đã thêm : Hàm Khởi tạo item
        private void InitItem()
        {
            if (item != null)
            {
                Item t = item;
                t.DestroyItem();
            }
            int i = new Random().Next(100);
            //Có 50% là item đạn
            if (i < 20 && i < 70)
                item = new AmmoItem();
            //có 40% là máu
            else if (i <= 70 && i < 90)
                item = new FirstAidItem();
            //có 10% là nâng cấp đạn
            else
                item = new UpgradeItem();
            item.CreateItem(MAP);
        }
        //Đã thêm : Mỗi 1 frame (1s) biến time
        private void tm_Tick(object sender, EventArgs e)
        {
            if (--timingControls <= 0)
            {
                InitItem();
                timingControls = 5;
            }
        }

        #region Nhóm hàm pause game
        private void ShowGameOver()
        {
            lbPause.Visible = !lbPause.Visible;
            btnResum.Visible = !btnResum.Visible;
            btnHome.Visible = !btnHome.Visible;
            btnQuit.Visible = !btnQuit.Visible;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            time.Stop();
            timerGame.Stop();
            ShowGameOver();
            btnPause.Enabled = false;
        }

        private void btnResum_Click(object sender, EventArgs e)
        {
            btnPause.Enabled = true;
            ShowGameOver();
            timerGame.Start();
            time.Start();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát chứ ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmMenu.Show();
        }
        #endregion

        private void FormGameplay_Load(object sender, EventArgs e)
        {

        }

        private void hp_Click(object sender, EventArgs e)
        {

        }
    }
}
