using Ninject;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using Xamarin.Forms;

namespace xamarinTesseract
{
    public partial class MainPage : ContentPage
    {
        private readonly ITesseractApi _tesseract;

        private readonly IProcessAssert _processAssert;

        public MainPage()
        {
            InitializeComponent();

            _tesseract = NinjectIoC.Container.Get<ITesseractApi>();

            _processAssert = NinjectIoC.Container.Get<IProcessAssert>();

            btn_submit.Clicked += Click;
        }

        private async void Click(object sender, EventArgs e)
        {
            
            await Recognise("img/tekcent.png");
        }

        public async Task Recognise(string path)
        {
            try
            {
                if (!_tesseract.Initialized)
                {
                    var initialised = await _tesseract.Init("eng");
                    if (!initialised)
                        return;
                }

                var stream = _processAssert.GetStreamByPath(path);

                if (!await _tesseract.SetImage(stream))
                {
                    
                }
                
                string text1 = _tesseract.Text;
                var words = _tesseract.Results(PageIteratorLevel.Word);
                var symbols = _tesseract.Results(PageIteratorLevel.Symbol);
                var blocks = _tesseract.Results(PageIteratorLevel.Block);
                var paragraphs = _tesseract.Results(PageIteratorLevel.Paragraph);
                var lines = _tesseract.Results(PageIteratorLevel.Textline);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
