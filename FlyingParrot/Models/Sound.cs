﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FlyingParrot.Models {
	public class Sound { 
		public int Id { get; set; }
		public string Text { get; set; }
		public string Filename { get; set; }
		public string Uploader { get; set; }
		public int? Category { get; set; }

		public Sound() { //sound constructor
			Id = 0;
			Text = "";
			Filename = "";
			Uploader = "";
		}

		public Sound(int id, string text, string filename, string uploader) { //sound initializer
			Id = id;
			Text = text;
			Filename = filename;
			Uploader = uploader;
		}

		public bool LoadData(int id) { 
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)) {
				con.Open();
				//get sound data
				SqlCommand soundCmd = new SqlCommand("SELECT [Id], [Text], [Filename], [Uploader], [Category] FROM Sounds WHERE [Id] = @Id", con);
				soundCmd.Parameters.Add("@Id", SqlDbType.Int).SqlValue = id;
				SqlDataReader soundReader = soundCmd.ExecuteReader();
				if (soundReader.HasRows) {
					while (soundReader.Read()) {
						Id = soundReader.GetInt32(0);
						Text = soundReader.GetString(1);
						Filename = soundReader.GetString(2);
						Uploader = soundReader.GetString(3);
						if (!soundReader.IsDBNull(4)) Category = soundReader.GetInt32(4);
					}
				}
				else {
					return false; //No rows found
				}
				soundReader.Close();

				return true;
			}
		}
        public static bool AddData(Sound NewSound) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)) {
                con.Open();
                SqlCommand soundCMD = new SqlCommand("INSERT INTO SOUNDS([FILENAME], [UPLOADER], [TEXT], [CATEGORY]) VALUES(@Filename, @Uploader, @Text, @Category)")
                    soundCMD.Parameters.Add("@Filename", SqlDbType.string).SqlValue = NewSound.Filename;
                soundCMD.Parameters.Add("@Uploader", SqlDbType.string).SqlValue = NewSound.Uploader;
                soundCMD.Parameters.Add("@Text", SqlDbType.string).SqlValue = NewSound.Text;
                soundCMD.Parameters.Add("@Category", SqlDbType.string).SqlValue = NewSound.Category;
                int Temp = soundCMD.ExecuteNonQuery();
                con.Close();
                if (Temp) return true;
                else return false;
            }
        }

		public static IEnumerable<Sound> LoadAll() {
			List<Sound> sounds = new List<Sound>();

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)) {
				con.Open();

				//get sound data
				SqlCommand soundCmd = new SqlCommand("SELECT [Id], [Text], [Filename], [Uploader], [Category] FROM Sounds", con);
				SqlDataReader soundReader = soundCmd.ExecuteReader();
				if (soundReader.HasRows) {
					while (soundReader.Read()) {
						Sound tempSound = new Sound (
							soundReader.GetInt32(0),
							soundReader.GetString(1),
							soundReader.GetString(2),
							soundReader.GetString(3)
						);
						if (!soundReader.IsDBNull(4)) tempSound.Category = soundReader.GetInt32(4);
						sounds.Add(tempSound);
					}
				}
				soundReader.Close();

				return sounds;
			}
		}
	}
}