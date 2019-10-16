using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyChangelog.Core.Interfaces;
using EasyChangelog.Core.Models;

namespace EasyChangelog.Tools
{
    public class VersionIncrementStrategy : IVersionIncrementStrategy
    {
        public Version NextVersion(Version lastVersion, ICollection<ConventionalCommit> conventionalCommits)
        {
            var versionImpact = GetVersionImpact(conventionalCommits);

            switch (versionImpact)
            {
                case VersionImpact.patch:
                    return new Version(lastVersion.Major, lastVersion.Minor, lastVersion.Build + 1);
                case VersionImpact.minor:
                    return new Version(lastVersion.Major, lastVersion.Minor + 1, 0);
                case VersionImpact.major:
                    return new Version(lastVersion.Major + 1, 0, 0);
                case VersionImpact.none:
                    return new Version(lastVersion.Major, lastVersion.Minor, lastVersion.Build + 1);
                default:
                    throw new InvalidOperationException($"Version impact of {versionImpact} cannot be handled");
            }
        }

        private VersionImpact GetVersionImpact(ICollection<ConventionalCommit> conventionalCommits)
        {
            // TODO: Quick and dirty implementation - Conventions? Better comparison?
            var versionImpact = VersionImpact.none;

            foreach (var conventionalCommit in conventionalCommits)
            {
                if (!string.IsNullOrWhiteSpace(conventionalCommit.Type))
                {
                    switch (conventionalCommit.Type)
                    {
                        case "fix":
                            versionImpact = MaxVersionImpact(versionImpact, VersionImpact.patch);
                            break;
                        case "feat":
                            versionImpact = MaxVersionImpact(versionImpact, VersionImpact.minor);
                            break;
                        default:
                            break;
                    }
                }

                if (conventionalCommit.Notes.Any(note => "BREAKING CHANGE".Equals(note.Title, StringComparison.InvariantCultureIgnoreCase)))
                {
                    versionImpact = MaxVersionImpact(versionImpact, VersionImpact.major);
                }
            }

            return versionImpact;
        }

        private VersionImpact MaxVersionImpact(VersionImpact impact1, VersionImpact impact2)
        {
            return (VersionImpact)Math.Max((int)impact1, (int)impact2);
        }
    }
}
