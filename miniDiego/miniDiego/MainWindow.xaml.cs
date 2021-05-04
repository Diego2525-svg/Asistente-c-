//Inicio del apartado de librerias y referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;//Salida de voz
using System.Speech.Recognition;//Reconocedor de voz
//Fin del apartado de librerias y refrencias
namespace miniDiego//Nombre del proyecto
{
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine reconocedor = new SpeechRecognitionEngine();//Este objeto reconocera la voz y las instrucciones
        SpeechSynthesizer miniDiego = new SpeechSynthesizer();//Este nos dara una respuesta 
        string speech;//Variable para interactuar con el asistente
        bool habilitarecnocimiennto = true;//Variable de tipo booleano para habilitar y deshabilitar el reconocimiento de voz

        public MainWindow()
        {
            InitializeComponent();//Iniciando sistema
            miniDiego.SpeakAsync("Iniciando");//Habla
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarGramaticas();//Se muestran las gramaticas en la ventana principal y se enlazan a todo el codigo
        }
        void cargarGramaticas()
        {
            reconocedor.RequestRecognizerUpdate();//Se activa el reconocimiento
            reconocedor.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices("Buenos dias","Abrir el google","Buenas noches","Desactivate","Abrir Youtube","Abrir Word"))));//Instrucciones
            reconocedor.SpeechRecognized += Reconocedor_SpeechRecognized;//Reconocimiento de voz
            miniDiego.SpeakStarted += MiniDiego_SpeakStarted;//Comienza habalar
            miniDiego.SpeakCompleted += MiniDiego_SpeakCompleted;//Termina hablar
            reconocedor.SetInputToDefaultAudioDevice();//Audio por defecto
            reconocedor.RecognizeAsync(RecognizeMode.Multiple);//Activacion y desactivacion de audio

        }

        private void MiniDiego_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            habilitarecnocimiennto = true;
        }

        private void MiniDiego_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
            habilitarecnocimiennto = false;//Reconocimiento inactivo
        }

        private void Reconocedor_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {  speech = e.Result.Text;//Se reconoce lo que dijo
            if (habilitarecnocimiennto==true//Reconocimiento activo)//Inicio if
            {
            switch (speech)
            {case ("Buenos dias")://Se dijo Buenos dias
             miniDiego.SpeakAsync("Buenos dias señor"+". . . .");//El asistente nos dice buenos dias
                    reconocimientolabel.Content = "";//Se crea un espacio en el label
                    reconocimientolabel.Content = speech;//Se pone la instruccion que recibio en el espacio creado
                    break;
             case("Abrir el google")://Se le pidio al asistente que abra el Google
                    miniDiego.SpeakAsync("Abriendo navegador" + ". . . .");//El asistente nos dice que empezo a abrir el Google
                    System.Diagnostics.Process.Start("https://www.google.es");//Se empieza a abrir el Google
                    reconocimientolabel.Content = "";//Se crea un espacio en el label
                    reconocimientolabel.Content = speech;//Se pone la instruccion que se recibio en espacio creado
                    break;
             case ("Buenas noches"):
                    miniDiego.SpeakAsync("Buenas noches señor" + ". . . .");
                    reconocimientolabel.Content = "";
                    reconocimientolabel.Content = speech;
                    break;
             case ("Desactivate"):
                        miniDiego.Speak("Hasta la proxima" + ". . . .");//Se le pidio al asistente que se desactive
                        reconocimientolabel.Content = "";//Se crea un espacio en el label
                        reconocimientolabel.Content = speech;//Se pone la instruccion que se recibio en espacio creado
                        Close();//Se cierra el prograna
                     break;
             case ("Abrir Youtube"):
                        miniDiego.SpeakAsync("Abriendo Youtube" + ". . . .");
                        System.Diagnostics.Process.Start("https://www.youtube.com");
                        reconocimientolabel.Content = "";
                        reconocimientolabel.Content = speech;
                        break;
             case ("Abrir Word"):
                        miniDiego.SpeakAsync("Abriendo Word" + ". . . .");
                        System.Diagnostics.Process.Start("winword.exe");
                        reconocimientolabel.Content = "";
                        reconocimientolabel.Content = speech;
                        break;

                    default:
                    break;
            }
            }//Fin if
        }
      
        
    }
}
