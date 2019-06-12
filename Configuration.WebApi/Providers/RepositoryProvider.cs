using System;
using LibGit2Sharp;

namespace Configuration.WebApi.Providers
{
	public interface IGitRepositoryProvider
	{
		void Clone();

		void Add(string filePath);

		void AddAll();

		void Commit(string message);

		void Push();

		void CommitAndPush(string message);

	}


	public class GitRepositoryProvider : IGitRepositoryProvider
	{
		private readonly Repository _repository;

		// var repo = Repository.Clone(Config.RepositoryUrl, Config.RepositoryPath);
		// var crap = Repository.Discover(Config.RepositoryPath);


		/// <inheritdoc />
		public void Clone()
		{
			Repository.Clone(Config.RepositoryUrl, Config.RepositoryPath);
		}

		/// <inheritdoc />
		public void Add(string filePath)
		{
			using (var repo = new Repository(Config.RepositoryPath))
			{
				// Stage the file
				repo.Index.Add(filePath);
				repo.Index.Write();
			}
		}

		/// <inheritdoc />
		public void AddAll()
		{
			using (var repo = new Repository(Config.RepositoryPath))
			{
				Commands.Stage(repo, "*");
			}
		}

		/// <inheritdoc />
		public void Commit(string message)
		{
			using (var repo = new Repository(Config.RepositoryPath))
			{
				// Create the committer's signature and commit
				var author = new Signature("Vajj", "email", DateTime.Now);
				var committer = author;

				// Commit to the repository
				var commit = repo.Commit(message, author, committer);
			}
		}

		/// <inheritdoc />
		public void Push()
		{
			using (var repo = new Repository(Config.RepositoryPath))
			{
				var options = new PushOptions
				{
					CredentialsProvider = (url, usernameFromUrl, types) =>
						new UsernamePasswordCredentials
						{
							Username = "vag1830",
							Password = "vaggelis1830"
						}
				};

				repo.Network.Push(repo.Branches["master"], options);
			}
		}

		/// <inheritdoc />
		public void CommitAndPush(string message)
		{
			Commit(message);
			Push();
		}
	}
}
