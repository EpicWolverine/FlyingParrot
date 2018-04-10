using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FlyingParrot.Models {
	public class Category {
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Sound> Sounds { get; set; }

		public Category() {
			Id = 0;
			Name = "";
		}

		public Category(int id, string name) {
			Id = id;
			Name = name;
		}

		public static IEnumerable<Category> LoadAll() {
			var categories = (List<Category>)LoadCategories();
			categories.ForEach(x => x.LoadSounds());
			return categories;
		}

		public bool LoadSounds() {
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)) {
				con.Open();
				//get category data
				SqlCommand nameCmd = new SqlCommand(@"SELECT [Name]
														FROM Categories
														WHERE Categories.[Id] = @Id", con);
				nameCmd.Parameters.Add("@Id", SqlDbType.Int).SqlValue = Id;
				Name = (string)nameCmd.ExecuteScalar();

				//get sound data
				SqlCommand soundCmd = new SqlCommand(@"SELECT Sounds.[Id], [Text], [Filename], [Uploader]
														FROM Sounds
														INNER JOIN Categories On Categories.[Id] = Sounds.[Category]
														WHERE Categories.[Id] = @Id", con);
				soundCmd.Parameters.Add("@Id", SqlDbType.Int).SqlValue = Id;
				SqlDataReader soundReader = soundCmd.ExecuteReader();
				if (soundReader.HasRows) {
					Sounds = new List<Sound>();
					while (soundReader.Read()) {
						Sound tempSound = new Sound(
							soundReader.GetInt32(0),
							soundReader.GetString(1),
							soundReader.GetString(2),
							soundReader.GetString(3)
						);
						Sounds.Add(tempSound);
					}
				}
				else {
					return false; //No rows found
				}
				soundReader.Close();

				return true;
			}
		}

		public static IEnumerable<Category> LoadCategories() {
			List<Category> categories = new List<Category>();

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)) {
				con.Open();

				//get category data
				SqlCommand cmd = new SqlCommand(@"SELECT [Id], [Name]
														FROM Categories", con);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows) {
					while (reader.Read()) {
						Category tempCategory = new Category(
							reader.GetInt32(0),
							reader.GetString(1)
						);
						categories.Add(tempCategory);
					}
				}
				reader.Close();

				return categories;
			}
		}
	}
}