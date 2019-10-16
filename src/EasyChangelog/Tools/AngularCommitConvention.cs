using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EasyChangelog.Core.Interfaces;
using EasyChangelog.Core.Models;
using EasyChangelog.Extensions;
using LibGit2Sharp;

namespace EasyChangelog.Tools
{
    public class AngularCommitConvention : ICommitConvention
    {
        static readonly string[] noteKeywords = new string[] { "BREAKING CHANGE" };

        private static readonly Regex HeaderPattern = new Regex("^(?<type>\\w*)(?:\\((?<scope>.*)\\))?: (?<subject>.*)$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public AngularCommitConvention()
        {
        }

        public ICollection<ConventionalCommit> Parse(ICollection<Commit> commits, Configuration config)
        {
            return commits.Select(c => Parse(c, config)).ToList();
        }

        public ConventionalCommit Parse(Commit commit, Configuration config)
        {
            var conventionalCommit = new ConventionalCommit();

            var commitMessageLines = commit.Message.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                )
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            var header = commitMessageLines.FirstOrDefault();

            if (header == null)
            {
                return conventionalCommit;
            }

            var match = HeaderPattern.Match(header);

            if (match.Success)
            {
                conventionalCommit.Scope = match.Groups["scope"].Value;
                conventionalCommit.Type = match.Groups["type"].Value;
                conventionalCommit.Subject = match.Groups["subject"].Value;
            }
            else
            {
                conventionalCommit.Subject = header;
            }

            for (var i = 1; i < commitMessageLines.Count; i++)
            {
                foreach (var noteKeyword in noteKeywords)
                {
                    var line = commitMessageLines[i];
                    if (line.StartsWith($"{noteKeyword}:"))
                    {
                        conventionalCommit.Notes.Add(new ConventionalCommitNote
                        {
                            Title = noteKeyword,
                            Text = line.Substring($"{noteKeyword}:".Length).TrimStart()
                        });
                    }
                }
            }

            conventionalCommit.Sha = commit.Sha;
            conventionalCommit.Url = GetCommitLink(commit, config);


            return conventionalCommit;
        }


        private string GetCommitLink(Commit commit, Configuration configuration)
        {

            var repoUrl = configuration.FirstOrDefault(c => c.Key.Equals("remote.origin.url"))?.Value;
            repoUrl = repoUrl.TrimEnd(".git");

            return $"{repoUrl}/commit/{commit.Sha}";
        }
    }
}
