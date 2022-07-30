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

        public string UserName { get; }

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
                Protocol = GitUrl.NetworkProtocol.Other
            }
        }
    }
}
