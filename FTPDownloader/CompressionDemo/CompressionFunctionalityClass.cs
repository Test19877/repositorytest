/*
 * Created by SharpDevelop.
 * User: igor.kabic
 * Date: 18/03/2015
 * Time: 17:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO.Compression;
using System.Collections.Generic;
using System.IO;

//using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;


namespace FTPDownloader
{
	/// <summary>
	/// Description of CompressionFunctionalityClass.
	/// </summary>
	public class CompressionFunctionalityClass
	{
		public void Unzip(string zipName, string dirToUnzipTo)
        {
            //This stores the path where the file should be unzipped to,
            //including any subfolders that the file was originally in.
            string fileUnzipFullPath;

            //This is the full name of the destination file including
            //the path
            string fileUnzipFullName;

            //Opens the zip file up to be read
            using (ZipArchive archive = ZipFile.OpenRead(zipName))
            {
                //Loops through each file in the zip file
                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    //Outputs relevant file information to the console
                    Console.WriteLine("File Name: {0}", file.Name);
                    Console.WriteLine("File Size: {0} bytes", file.Length);
                    Console.WriteLine("Compression Ratio: {0}", ((double)file.CompressedLength / file.Length).ToString("0.0%"));

                    //Identifies the destination file name and path
                    fileUnzipFullName = Path.Combine(dirToUnzipTo, file.FullName);

                    //Extracts the files to the output folder in a safer manner
                    if (!System.IO.File.Exists(fileUnzipFullName))
                    {
                        //Calculates what the new full path for the unzipped file should be
                        fileUnzipFullPath = Path.GetDirectoryName(fileUnzipFullName);
                        
                        //Creates the directory (if it doesn't exist) for the new path
                        Directory.CreateDirectory(fileUnzipFullPath);

                        //Extracts the file to (potentially new) path
                        file.ExtractToFile(fileUnzipFullName);
                    }
                }
            }
        }
		

		
		public void GzTarExtract(string gzipFileName, string targetDir)
        {
            byte[ ] dataBuffer = new byte[4096];

        	using (System.IO.Stream fs = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read)) 
        	{
		     	
	            using (GZipInputStream gzipStream = new GZipInputStream(fs)) 
	            {
	
	               
		            string fnOut = Path.Combine(targetDir, Path.GetFileNameWithoutExtension(gzipFileName));
		
		            using (FileStream fsOut = File.Create(fnOut)) 
		            {
		                    StreamUtils.Copy(gzipStream, fsOut, dataBuffer);
	            	}	
	    		}	
    		}
        	string gzfileNameWithoutExtension = Path.GetFileNameWithoutExtension(gzipFileName);
	      	var tarFullPathFileName = targetDir + "\\" + gzfileNameWithoutExtension ;
	      	Stream inStream = File.OpenRead(@tarFullPathFileName);

	        TarArchive tarArchive = TarArchive.CreateInputTarArchive(inStream);
	        tarArchive.ExtractContents(targetDir);
	        tarArchive.Close();
	
	        inStream.Close();
	        
	        if (File.Exists(@tarFullPathFileName))
		            	{
		            		File.Delete(@tarFullPathFileName);	 	            		
		            	}
        }
		
	
	}
}