namespace xamarinTesseract
{
    using System.IO;
   
    public  interface IProcessAssert
    {
        Stream GetStreamByPath(string Path);
    }
}
