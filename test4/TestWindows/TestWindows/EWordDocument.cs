using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    public class EWordDocument
    {
        public List<string> LText;
        public void Open(string path)
        {
            //语句结束后，自动释放WordDocument，这里不让它消毁
            //using (WordDocument = WordprocessingDocument.Open(path, false))
            WordprocessingDocument WordDocument = WordprocessingDocument.Open(path, false);
            Body body = WordDocument.MainDocumentPart.Document.Body;
            if (body.Elements() == null)
                return;

            LText = new List<string>();

            foreach (DocumentFormat.OpenXml.OpenXmlElement obj in WordDocument.MainDocumentPart.Document.Body.Elements())
            {
                if (obj is Paragraph)
                {//段落
                    string PrgText = null;
                    Paragraph paragraph = (Paragraph)obj;
                    string str = null;
                    foreach (Text text in paragraph.Descendants<Text>())
                    {
                        str += text.Text;
                    }
                    if (PrgText == null)
                        PrgText = str;
                    else
                        PrgText += $"\n{str}";
                    LText.Add(PrgText);
                }
                else if (obj is Table)
                {//表格                  
                }
                else if (obj is SectionProperties)
                {//页面属性
                }
            }
        }
    }
}
