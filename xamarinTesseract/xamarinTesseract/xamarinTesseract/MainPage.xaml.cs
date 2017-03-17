using Ninject;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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

        //private string _path = "img/1.png";

        public MainPage()
        {
            InitializeComponent();

            _tesseract = NinjectIoC.Container.Get<ITesseractApi>();

            _processAssert = NinjectIoC.Container.Get<IProcessAssert>();

            //img.Source = ImageSource.FromStream(() => { return _processAssert.GetStreamByPath(_path); });

            btn_submit.Clicked += Click;
        }

        private async void Click(object sender, EventArgs e)
        {
            string uuid = Guid.NewGuid().ToString();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                PhotoSize = PhotoSize.Small,
                //CustomPhotoSize = 80
            });

            if (file == null)
                return;

            //img.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    file.Dispose();
            //    return stream;
            //});

            //byte[] imageAsBytes = null;
            //using (var memoryStream = new MemoryStream())
            //{
            //    file.GetStream().CopyTo(memoryStream);
            //    file.Dispose();
            //    imageAsBytes = memoryStream.ToArray();
            //}


            await Recognise(file.GetStream());
        }

        public async Task Recognise(Stream stream)
        {
            if (!_tesseract.Initialized)
            {
                var initialised = await _tesseract.Init("eng");
                if (!initialised)
                    return;
            }

            if (!await _tesseract.SetImage(stream))
            {
                //TO DO Wrong image
            }

            lbl_text.Text = _tesseract.Text;
            var words = _tesseract.Results(PageIteratorLevel.Word);
            var symbols = _tesseract.Results(PageIteratorLevel.Symbol);
            var blocks = _tesseract.Results(PageIteratorLevel.Block);
            var paragraphs = _tesseract.Results(PageIteratorLevel.Paragraph);
            var lines = _tesseract.Results(PageIteratorLevel.Textline);
        }
    }
}
