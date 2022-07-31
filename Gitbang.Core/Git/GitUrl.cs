using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Core.Git
{
    public class GitUrl
    {
        private const string GitPrefix = "git@";
        private const string VisualStudioDotComHostName = "@vs-ssh.visualstudio.com";

        public string Url { get; }

        public GitNetworkProtocol Protocol { get; }

        public Uri Uri { get; }

        public string Host { get; }

        public string Username { get; }

        public string Slug { get; }

        public string RepositoryName { get; }

        public bool IsValid => string.IsNullOrEmpty(RepositoryName);

        public GitUrl(string urlString)
        {
            Url  = urlString;
            var uriString = urlString;

            var gitPrefix = "git@";

            if (uriString.StartsWith(gitPrefix))
            {
                uriString = $"ssh://{uriString.Substring(gitPrefix.Length).Replace(":", "/")}";
            }
            else if (uriString.Contains(VisualStudioDotComHostName))
            {
                uriString = $"ssh://{uriString.Replace("@vs-ssh", "").Replace(":", "/")}";
            }

            Uri result;
            
            if (!Uri.TryCreate(uriString, UriKind.Absolute, out result))
            {
                Host = null;
                Protocol = GitNetworkProtocol.Other;
                Uri = null;
                Username = null;
                return;
            }

            Host = result.Host.ToLower();
            Protocol = GetProtocol(result.Scheme);
            Uri = result;
            Username = ParseUsername(result);
            var text = Uri.LocalPath.TrimStart('/');

            if(text.EndsWith(".git"))
            {
                text = text.Substring(0, text.Length - ".git".Length);
            }
            Slug = text;

            var array = text.Split('/');
            if(array.Length >= 2)
            {
                RepositoryName = array[array.Length - 1];
                return;
            }
            
            if(array.Length == 1)
            {
                RepositoryName = array[0];
            }
        }

        private static string ParseUsername(Uri uri)
        {
            if(uri.Scheme == "ssh")
            {
                return null;
            }
            
            var userInfo = uri.UserInfo;
            if(string.IsNullOrEmpty(userInfo))
            {
                return null;
            }
            
            var num = userInfo.IndexOf(":");

            if(num==-1)
            {
                return userInfo;
            }

            if(num == 0)
            {
                return null;
            }
            
            return userInfo.Substring(0, num);
        }

        private static GitNetworkProtocol GetProtocol(string scheme)
        {
            switch(scheme)
            {
                case "https":
                    {
                        return GitNetworkProtocol.Https;
                    }
                case "ssh":
                    {
                        return GitNetworkProtocol.Ssh;
                    }
                default:
                    return GitNetworkProtocol.Other;
            }
        }
        
    }

   
}
