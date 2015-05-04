/*
 * Created by SharpDevelop.
 * User: igor.kabic
 * Date: 18/03/2015
 * Time: 12:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.IO;

namespace FTPDownloader
{
	/// <summary>
	/// Description of FtpFunctionalityClass.
	/// </summary>
	public class FtpFunctionalityClass
	{
		// method checks if file exists on ftp server
		public bool CheckIfFtpFileExists(string fileFullPath, string userNameFtp, string userPasswordFtp)
		{
    	var request = (FtpWebRequest)WebRequest.Create (fileFullPath);
    	request.Credentials = new NetworkCredential (userNameFtp, userPasswordFtp);
		request.Method = WebRequestMethods.Ftp.GetFileSize;
		try
			{
			    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
			}
			catch (WebException ex)
			{
			    FtpWebResponse response = (FtpWebResponse)ex.Response;
			    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
			    {
		 			return false;
			    }
		    }
		return true;
		}
		
		
	public bool DownloadedFileFromFtpServer(string fileFullPath, string userNameFtp, string userPasswordFtp, string outputDirectory)
	{
		
		string fileName = Path.GetFileName(fileFullPath);
		FtpFunctionalityClass cs = new FtpFunctionalityClass();
		bool fileExists = cs.CheckIfFtpFileExists(fileFullPath, userNameFtp, userPasswordFtp);
		
		if (fileExists)
		{
			// conecting to ftp server and downloading source file
			using (WebClient ftpClient = new WebClient())
			{
			    ftpClient.Credentials = new System.Net.NetworkCredential(userNameFtp, userPasswordFtp);
			    ftpClient.DownloadFile(fileFullPath, outputDirectory + fileName);
			    return true;
			}
		}
		return false;
	}
		
	}
}
