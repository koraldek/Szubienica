using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Szubienica
{
    public sealed partial class MainPage : Page
    {
        String[] passwords = {"ADWOKAT BOGA"
,"AFERA ROZPORKOWA"
,"GABINET CIENI"
,"MANNA Z NIEBA"
,"MGNIENIE OKA"
,"BABA JAGA"
,"NAWIGACJA"
,"SZCZUR"
,"CHODNIK"
,"REKOWALESCENCJA"
,"MOTYW"
,"NEUTRALIZOWANIE"
,"ASPIRYNA"
,"CUKIER"
,"PTAK"
,"ONOMATOPEJA"
,"POCHODNA"
,"EKSTREMUM"
,"MINIMUM"
,"KOT"
,"GNIAZDO"
,"MOTOCYKL"
,"HIGIENA OSOBISTA"
,"GOLARKA"
,"MASZYNKA"
,"KOMPUTER"
,"ANTYTERRORYSTA"
,"POLICJA ENERGETYCZNA"
,"ELEKTROWNIA"
,"ZALOTKA"};
        String dispPass = "";
        String password;
        int passL, chances, tips;
        const int ZERO_CHANCES = 0;
        public MainPage()
        {
            Random r = new Random();
            password = passwords[r.Next(passwords.Length)];
            chances = 9;
            passL = password.Length;
            int diffCharacters = password.Distinct().Count();

            tips = diffCharacters / 5 + 1;
            for (int i = 0; i < passL; i++) // zamiana na kreski
                dispPass = dispPass + "_";
            this.InitializeComponent();
            TipB.Content = "Tip (" + tips + ")";
            parsePass(dispPass);
            footerTxt.Text = "You have " + chances + " , good luck!";
        }

        private void checkCharacter(RoutedEventArgs e)
        {
            bool charFound = false;
            Button btn = (Button)e.OriginalSource;
            String c = (String)btn.Content;
            char inpChar = c[0];
            char[] newDispPass = dispPass.ToCharArray();

            for (int i = 0; i < passL; i++)
                if (password[i].Equals(inpChar))
                {
                    newDispPass[i] = inpChar;
                    charFound = true;
                }

            dispPass = new string(newDispPass);
            parsePass(dispPass);

            if (isCompleted())
                wonGame();
            else if (chances == ZERO_CHANCES)
                lostGame();
            else if (charFound)
                ;
            else
            {
                chances--;
                int number = Math.Abs(chances - 9);
                szubienicaImage.Source = new BitmapImage(new Uri("ms-appx:///res/img/s" + number + ".bmp", UriKind.Absolute));
                footerTxt.Text = "You have " + chances + " chances, good luck!";
            }

            btn.IsEnabled = false;
        }

        private bool isCompleted()
        {
            String pass = password.Replace(" ", "_");
            if (pass.Equals(dispPass))
                return true;
            else
                return false;
        }

        private void parsePass(String passToDisp)
        {
            String newPass ="";
            for(int i=0;i<passL;i++)
            {
                if (password[i].Equals(' '))
                    newPass = newPass + " ";
                else
                    newPass = newPass + dispPass[i];
            }
            passBox.Text = newPass;
        }

        private async void wonGame()
        {
            MessageDialog msgbox = new MessageDialog("You won! Would you like to play new game?", "GOOD GAME!");

            msgbox.Commands.Clear();
            msgbox.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            msgbox.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var res = await msgbox.ShowAsync();

            if ((int)res.Id == 0)
                this.Frame.Navigate(typeof(MainPage));
            else if ((int)res.Id == 1)
                CoreApplication.Exit();
        }

        private async void lostGame()
        {
            MessageDialog msgbox = new MessageDialog(" You are dead. Password is:" + password + " Want revenge?", "Failed");

            msgbox.Commands.Clear();
            msgbox.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            msgbox.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var res = await msgbox.ShowAsync();

            if ((int)res.Id == 0)
                this.Frame.Navigate(typeof(MainPage));
            else if ((int)res.Id == 1)
                CoreApplication.Exit();
        }

        private void NewGameB_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void TipB_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;

            if (tips > 0)
            {
                char[] newDispPass = dispPass.ToCharArray();
                Random r = new Random();
                int charIndex;
                char showChar;
                do
                {
                    charIndex = r.Next(password.Length);
                    showChar = password[charIndex];
                }
                while (dispPass[charIndex].Equals(password[charIndex]) || password[charIndex].Equals(' '));

                for (int i = 0; i < passL; i++)
                    if (password[i].Equals(showChar))
                        newDispPass[i] = showChar;

                dispPass = new string(newDispPass);
                parsePass(dispPass);
                if (isCompleted())
                    wonGame();
                String btnName = "b" + showChar.ToString();

                Button charBtn = (Button)FindControl<Button>(this, typeof(Button), btnName);
                charBtn.IsEnabled = false;
            }
            tips--;
            TipB.Content = "Tip (" + tips + ")";
            if (tips == 0)
            {
                btn.IsEnabled = false;
                btn.Content = "No more tips";
            }
        }

        private void GiveUpB_Click(object sender, RoutedEventArgs e)
        {
            lostGame();
        }

        private void cbA(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }

        private void cbB(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }

        private void cbC(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }

        private void cbD(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbE(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbF(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }

        private void cbG(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }

        private void cbH(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbI(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbJ(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbK(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbL(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbM(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbN(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbO(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbP(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbQ(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbR(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbS(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbT(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbU(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }

        private void cbW(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbV(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbX(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbY(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }
        private void cbZ(object sender, RoutedEventArgs e)
        {
            checkCharacter(e);
        }

        public static T FindControl<T>(UIElement parent, Type targetType, string ControlName) where T : FrameworkElement
        {

            if (parent == null) return null;

            if (parent.GetType() == targetType && ((T)parent).Name == ControlName)
            {
                return (T)parent;
            }
            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);

                if (FindControl<T>(child, targetType, ControlName) != null)
                {
                    result = FindControl<T>(child, targetType, ControlName);
                    break;
                }
            }
            return result;
        }

    }
}
