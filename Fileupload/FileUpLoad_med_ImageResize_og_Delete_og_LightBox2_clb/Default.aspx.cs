using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region simpel upload til mappe og database

    protected void Button_upload_Click(object sender, EventArgs e)
    {
        try
        {
            // Upload originalen
            FileUpload_img.SaveAs(Server.MapPath("~/Images/Upload/Original/") + FileUpload_img.FileName);

            // Insert i databasen
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Media (ImageFileName) VALUES (@ImageFileName)";
            cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar).Value = FileUpload_img.FileName;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Kald Metoden MakeThumbs, som laver en Reisze af originalen til 768px i bredden og uploader den til Resized_original mappen 
            MakeThumb(FileUpload_img.FileName, "/Images/Upload/Original/", 768, "/Images/Upload/Resized_Original/");

            // Kald Metoden MakeThumbs, som laver en Thumbnail og uploader den til Thumbs mappen
            MakeThumb(FileUpload_img.FileName, "/Images/Upload/Original/", 120, "/Images/Upload/Thumbs/");

            // Besked om at billedet er gemt
            Label_besked.Text = "Billedet blev gemt: ";
        }
        catch (Exception)
        {
            Label_besked.Text = "Billedet blev <b>ikke</b> gemt: ";
        }
    }

    #endregion

    #region upload med GUID filnavn

    protected void Button_Dynamisk_filenavn_Click(object sender, EventArgs e)
    {
        try
        {
            // Opret en tilfældig tekst streng 
            Guid TilfealdigtFilNavn = Guid.NewGuid();

            #region find filtypenavnet

            // Få fat i filtypenavnet
            // Gem hele filnavnet i en streng
            string fileTypeNavn = FileUpload_img.FileName;

            // opdel strengen i 2 dele (navnet og filtypeendelsen (jog, gif, png)) og gem det i en liste
            // Split('.') klipper strengen over der hvor der er et punktum
            List<string> endelsen = new List<string>(fileTypeNavn.Split('.'));

            // find filtypeendelsen
            string filTypeEndelse = endelsen[endelsen.Count - 1];

            // Hele det nye filnavn med filtypeendelsen
            string billedUrl = TilfealdigtFilNavn + "." + filTypeEndelse;

            #endregion

            FileUpload_img.SaveAs(Server.MapPath("~/Images/upload/Original/") + billedUrl);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Media (ImageFileName) VALUES (@ImageFileName)";
            cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar).Value = billedUrl; // denne linie er blevet ændret
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Kald Metoden MakeThumbs, som laver en Reisze af originalen til 768px i bredden og uploader den til Resized_original mappen 
            MakeThumb(billedUrl, "/Images/Upload/Original/", 768, "/Images/Upload/Resized_Original/");

            // Kald Metoden MakeThumbs, som laver en Thumbnail og uploader den til Thumbs mappen
            MakeThumb(billedUrl, "/Images/Upload/Original/", 120, "/Images/Upload/Thumbs/");

            // Besked om at billedet er gemt
            Label_besked.Text = "Billedet blev gemt: ";
        }
        catch (Exception)
        {

            Label_besked.Text = "Billedet blev <b>ikke</b> gemt: ";
        }

    }

    #endregion

    #region  upload med DATO filnavn
    protected void Button_upload_dato_Click(object sender, EventArgs e)
    {

        try
        {
            // Find dato og tid lige nu og gem det i en string
            string dato = DateTime.Now.ToString();
            // Skift mellemrum og kolon ud med streg og undescore
            dato = dato.Replace(" ", "-");
            dato = dato.Replace(":", "_");

            #region find filtypenavnet

            // Få fat i filtypenavnet
            // Gem hele filnavnet i en streng
            string fileTypeNavn = FileUpload_img.FileName;

            // opdel strengen i 2 dele (navnet og filtypeendelsen (jog, gif, png)) og gem det i en liste
            // Split('.') klipper strengen over der hvor der er et punktum
            List<string> endelsen = new List<string>(fileTypeNavn.Split('.'));

            // find filtypeendelsen
            string filTypeEndelse = endelsen[endelsen.Count - 1];

            // Hele det nye filnavn med filtypeendelsen
            string billedUrl = dato + "." + filTypeEndelse;

            #endregion

            FileUpload_img.SaveAs(Server.MapPath("~/Images/upload/Original/") + billedUrl);
            
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Media (ImageFileName) VALUES (@ImageFileName)";
            cmd.Parameters.Add("@ImageFileName", SqlDbType.NVarChar).Value = billedUrl; // denne linie er blevet ændret
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Kald Metoden MakeThumbs, som laver en Reisze af originalen til 768px i bredden og uploader den til Resized_original mappen 
            MakeThumb(billedUrl, "/Images/Upload/Original/", 768, "/Images/Upload/Resized_Original/");

            // Kald Metoden MakeThumbs, som laver en Thumbnail og uploader den til Thumbs mappen
            MakeThumb(billedUrl, "/Images/Upload/", 120, "/Images/Upload/Thumbs/");

            // Besked om at billedet er gemt
            Label_besked.Text = "Billedet blev gemt: ";
        }
        catch (Exception)
        {

            Label_besked.Text = "Billedet blev <b>ikke</b> gemt: ";
        }

    }

    #endregion

    #region Metoden MakeThumb
    /// <summary>
    /// Opret et Thumb, baseret på en fil og en mappe
    /// </summary>
    /// <param name="Filename">Hvad hedder filen</param>
    /// <param name="UploadFolder">Hvor er den uploadet til</param>
    private void MakeThumb(string Filename, string UploadFolder, int bredde, string thumbuploadfolder)
    {
        // find det uploadede image
        System.Drawing.Image OriginalImg = System.Drawing.Image.FromFile(Server.MapPath("~/") + UploadFolder + Filename);

        // find højde og bredde på image
        int originalWidth = OriginalImg.Width;
        int originalHeight = OriginalImg.Height;

        // bestem den nye bredde på det thumbnail som skal laves
        int newWidth = bredde;

        // beregn den nye højde på thumbnailbilledet
        double ratio = newWidth / (double)originalWidth;
        int newHeight = Convert.ToInt32(ratio * originalHeight);


        Bitmap Thumb = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
        Thumb.SetResolution(OriginalImg.HorizontalResolution, OriginalImg.VerticalResolution);

        // Hvis billedet indeholder nogen form for transperans 
        //(mere eller mindre gennemsigtig, eller en gennemsigtig baggrund) bliver det gjort her
        Thumb.MakeTransparent();


        // opret det nye billede
        Graphics ThumbMaker = Graphics.FromImage(Thumb);
        ThumbMaker.InterpolationMode = InterpolationMode.HighQualityBicubic;

        ThumbMaker.DrawImage(OriginalImg,
            new Rectangle(0, 0, newWidth, newHeight),
            new Rectangle(0, 0, originalWidth, originalHeight),
            GraphicsUnit.Pixel);

        // encoding
        ImageCodecInfo encoder;
        string fileExt = System.IO.Path.GetExtension(Filename);
        switch (fileExt)
        {
            case ".png":
                encoder = GetEncoderInfo("image/png");
                break;

            case ".gif":
                encoder = GetEncoderInfo("image/gif");
                break;

            default:
                // default til JPG 
                encoder = GetEncoderInfo("image/jpeg");
                break;
        }

        EncoderParameters encoderParameters = new EncoderParameters(1);
        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

        // gem thumbnail i mappen /Images/Uploads/Thumbs/
        Thumb.Save(Server.MapPath("~") + thumbuploadfolder + Filename, encoder, encoderParameters);

        // Fjern originalbilledet, thumbnail mm, fra computerhukommelsen
        OriginalImg.Dispose();
        ThumbMaker.Dispose();
        Thumb.Dispose();

    }

    #region encoding metode

    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
        for (int i = 0; i < encoders.Length; i++)
        {
            if (encoders[i].MimeType == mimeType)
            {
                return encoders[i];
            }
        }
        return null;
    }
    #endregion

    #endregion

}