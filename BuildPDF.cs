using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSharp_Test_With_Console_APP
{
    internal class BuildPDF
    {
       
        public void gerarPDF(string nome, int idade, string cargo)
        {


            // Criando o documento PDF
            PdfDocument document = new PdfDocument();
            document.Info.Title = "DOC Corporativo";

            // Adicionando uma nova página ao documento
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromMillimeter(74);
            page.Height = XUnit.FromMillimeter(52);
            page.Orientation = PageOrientation.Portrait;

            // Obtendo o objeto XGraphics para desenhar na página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Desenhando o fundo do cartão
            XRect rect = new XRect(0, 0, page.Width, page.Height);
            XColor corFundo = XColor.FromArgb(0, 196, 195);
            XSolidBrush brush = new XSolidBrush(corFundo);
            gfx.DrawRectangle(brush, rect);

            // Adicionando as informações do cartão
            XFont font = new XFont("Verdana", 10, XFontStyle.Bold);
            XBrush brush2 = XBrushes.White;

            gfx.DrawString($"Nome: {nome}", font, brush2, new XRect(10, 10, page.Width - 20, 20), XStringFormats.TopLeft);
            gfx.DrawString($"Idade: {idade}", font, brush2, new XRect(10, 30, page.Width - 20, 20), XStringFormats.TopLeft);
            gfx.DrawString($"Cargo: {cargo}", font, brush2, new XRect(10, 50, page.Width - 20, 20), XStringFormats.TopLeft);

            // Desenhando a barra branca no final do documento
            XRect rect2 = new XRect(0, page.Height - 5, page.Width, 5);
            XSolidBrush brush3 = new XSolidBrush(XColor.FromArgb(255, 255, 255));
            gfx.DrawRectangle(brush3, rect2);

            // Salvando o documento
            string filename = "CartaoCorporativo.pdf";
            document.Save(filename);

            // Abrindo o documento gerado
            Process.Start(filename);


        }
    }
}
