using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
       	
//using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace FTPDownloader
{

    class Program
    {
        
        static void Main(string[] args)
        {	
        	// variables initialization
        	

		
           	
			//CompressionFunctionalityClass cs2 = new CompressionFunctionalityClass();
			//cs2.GzTarExtract( @"C:\Users\igor.kabic\Desktop\tradglobalprod_2015-04-27-lookup_data.tar.gz", @"C:\Users\igor.kabic\Desktop\New Folder");
        	//cs2.Unzip(@"C:\Users\igor.kabic\Desktop\merchant_traffic_germany_20150428.zip", @"C:\Users\igor.kabic\Desktop\New Folder");
        	//cs2.Unzip(@"C:\Users\igor.kabic\Desktop\merchant_traffic_germany_20150428.zip", @"C:\Users\igor.kabic\Desktop\New Folder");
        	
        	//string sourceFullPath = "ftp://ftp.shtab.org/repository/100godina.zip";
            string sourceFullPath = "ftp://ftp.shtab.org/repository/tradglobalprod_2015-04-27-lookup_data.tar.gz";
            //string sourceFullPath = "ftp://54.93.175.147/de/sitecatalyst/product_traffic_spain_20150324.csv";
          
            string userNameFtp = "oi-plamen@shtab.org";
            string userPasswordFtp = "oscarindiapapa71";

        	//string outputDirectory = @"Y:\FTPDownloader\";
        	//string outputDirectory = @"D:\Tmp\sitecatalyst\de_merchant_traffic_orders\";
        	string outputDirectory = @"C:\Users\igor.kabic\Desktop\New Folder";
        	//string outputDirectory = "D:\\Tmp\\sitecatalyst\\de_merchant_traffic_orders\\";
        	bool flgFileIsZip = true;
        	bool flgCheckAvailability = false;
        	
        	//FTPDownloader.exe "ftp://54.93.175.147/de/sitecatalyst/merchant_orders_germany_2015031.csv" "sitecatalyst" "w9isODSaGbU" "D:\\Tmp\\sitecatalyst\\de_merchant_traffic_orders\\" "false" "false"
        	//FTPDownloader.exe "ftp://ftp.shtab.org/repository/tradglobalprod_2015-04-27-lookup_data.tar.gz" "oi-plamen@shtab.org" "oscarindiapapa71" "C:\\Users\\igor.kabic\\Desktop\\New Folder\\" "true" "false"
        	//FTPDownloader.exe "ftp://ftp.shtab.org/repository/100godina.zip" "oi-plamen@shtab.org" "oscarindiapapa71" "C:\\Users\\igor.kabic\\Desktop\\New Folder\\" "true" "false"
        	
       
       /* 	
   
       		
  			string sourceFullPath = args[0];
        	string userNameFtp = args[1];
        	string userPasswordFtp = args[2];
        	string outputDirectory = args[3];
        	string arg4 = args[4];
        	string arg5 = args[5];
        	bool flgFileIsZip = false;
        	bool flgCheckAvailability = false;
        	
        	if (arg4=="true")
        	{
        		flgFileIsZip = true;
        	}
        	
        	if (arg5=="true")
        	{
        		flgCheckAvailability = true;
        	}
            */
			
        	
			bool flgFileExists = false;
        	string fileName = Path.GetFileName (sourceFullPath);
        	string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFullPath);
        	string fileExtension = Path.GetExtension(sourceFullPath);
        
        	FtpFunctionalityClass cs = new FtpFunctionalityClass();
        	CompressionFunctionalityClass cs2 = new CompressionFunctionalityClass();
        	ConnectionToSQL cs3 = new ConnectionToSQL();
        	Console.WriteLine(@outputDirectory + fileName);
        	
        

        	flgFileExists = cs.CheckIfFtpFileExists(sourceFullPath, userNameFtp, userPasswordFtp);
        	        	
			if (flgFileExists)
			{
				if (flgCheckAvailability)
				{
					Console.WriteLine("File \"" + fileName + "\" exists. ");
					Console.ReadKey(true);
				}
				else
				{
					
					bool fileDownloaded = cs.DownloadedFileFromFtpServer (sourceFullPath, userNameFtp, userPasswordFtp, outputDirectory);
					if (fileDownloaded )
					{
						Console.WriteLine("File \"" + fileName + "\" downloaded.");
						//Console.ReadKey(true);
						
					}
					else
					{
						Console.WriteLine("File \"" + fileName + "\" is not downloaded.");
						//Console.ReadKey(true);
					}
					if (fileExtension != ".gz") 
					{
						cs3.ConfigFTPDownladerTableInsert(fileName, "true");	
					}
		            
		    		// extracting zip files
		    		// for ExtractToDirectory function System.IO.Compression.FileSystem reference has to be imported
		            if (flgFileIsZip)
		            {	
						
		            	if (fileExtension == ".gz") 
		            	{
		            		Console.WriteLine("File gztar \"" + fileName + "\" extracted.");
		            		//Console.ReadKey(true);
		            		cs2.GzTarExtract(@outputDirectory + fileName, @outputDirectory);
		            	
			            	
		            	}
		            	else
		            	{
		            		Console.WriteLine("File \"" + fileName + "\" extracted.");
		            		//Console.ReadKey(true);
		            		cs2.Unzip(@outputDirectory + fileName, @outputDirectory);
		            	
			            	/*using (ZipArchive archive = ZipFile.OpenRead(@outputDirectory + fileName))
	            			{
				                //Loops through each file in the zip file
				                foreach (ZipArchiveEntry file in archive.Entries)
				                {
				                	cs3.ConfigFTPDownladerTableInsert(file.Name, "true");
				                	//Console.ReadKey(true);
				                }
			            	}
			            	*/
		            	}
		            	
		            	if (File.Exists(@outputDirectory + fileName))
		            	{
		            		File.Delete(@outputDirectory + fileName);	 	            		
		            	}
		            }
		         
				}
			}
			else 
			{
				cs3.ConfigFTPDownladerTableInsert (fileName, "false"); 
			}
		}
    }
}




