using InventarisDomain;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartschoolProject.Data
{
    public class WordService
    {

        public MemoryStream CreateWord(List<Lokaal> lokalen)
        {
            //Creating a new document
            WordDocument document = new WordDocument();
            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 16f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 18f;
            style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 12;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            WTextRange textRange = paragraph.AppendText("Overzicht") as WTextRange;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);

            //Appends paragraph
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Inventaris Lokalen") as WTextRange;
            textRange.CharacterFormat.FontSize = 20f;
            textRange.CharacterFormat.FontName = "Calibri";

            //Eerste cel van tabel
            IWTable table = section.AddTable();
            table.ResetCells(lokalen.Count(), 3);
            WTableCell firstCell = table.Rows[0].Cells[0];
            firstCell.Width = 100;
            paragraph = firstCell.AddParagraph();
            paragraph.ApplyStyle("Normal");
            textRange = paragraph.AppendText("Lokaal") as WTextRange;
            textRange.CharacterFormat.Bold = true;

            //Tweede cel van tabel
            WTableCell secondCell = table.Rows[0].Cells[1];
            secondCell.Width = 180;
            paragraph = secondCell.AddParagraph();
            paragraph.ApplyStyle("Normal");
            textRange = paragraph.AppendText("Details") as WTextRange;
            textRange.CharacterFormat.Bold = true;

            //Derde cel van tabel
            WTableCell thirdCell = table.Rows[0].Cells[2];
            thirdCell.Width = 170;
            paragraph = thirdCell.AddParagraph();
            paragraph.ApplyStyle("Normal");
            textRange = paragraph.AppendText("Voorwerpen") as WTextRange;
            textRange.CharacterFormat.Bold = true;

            for (int i = 1; i < lokalen.Count(); i++) 
            {

                //Lokaal
                firstCell = table.Rows[i].Cells[0];
                firstCell.Width = 100;
                paragraph = firstCell.AddParagraph();
                textRange = paragraph.AppendText("Lokaal " + lokalen[i].lokaalNr) as WTextRange;
                textRange.CharacterFormat.FontSize = 14f;

                //Details per lokaal
                secondCell = table.Rows[i].Cells[1];
                secondCell.Width = 180;
                paragraph = secondCell.AddParagraph();
                textRange = paragraph.AppendText("Nummer : " + lokalen[i].lokaalNr + "\n" +
                                                 "Verantwoordelijke : " + lokalen[i].lokaalVerantwoordelijke + "\n" +
                                                 "Actief/Inactief : " + (lokalen[i].isActief ? "Actief" : "Inactief")) as WTextRange;
                textRange.CharacterFormat.FontSize = 13f;

                //Voorwerpen per lokaal
                for (int j = 0; j < lokalen[i].Voorwerpen.Count(); j++)
                {
                    thirdCell = table.Rows[i].Cells[2];
                    thirdCell.Width = 170;
                    paragraph = thirdCell.AddParagraph();
                    textRange = paragraph.AppendText("- " + lokalen[i].Voorwerpen[j].aantal + " x " + lokalen[i].Voorwerpen[j].voorwerpNaam + "\n" +
                                                            (lokalen[i].Voorwerpen[j].defect ? "defect" : "niet defect")) as WTextRange;
                    textRange.CharacterFormat.FontSize = 13f;
                }

            }

            //Saves the Word document to MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);
            //Closes the Word document
            document.Close();
            stream.Position = 0;

            return stream;
        }

    }
}
