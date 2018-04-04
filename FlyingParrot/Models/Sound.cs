using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FlyingParrot.Models {
    public class Sound {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Uploader { get; set; }

        public Sound() {
            Id = 0;
            Filename = "";
            Uploader = "";
        }

        public Sound(int id, string filename, string uploader) {
            Id = id;
            Filename = filename;
            Uploader = uploader;
        }

        public bool LoadData(int id) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)) {
                con.Open();
                //get sound data
                SqlCommand soundCmd = new SqlCommand("SELECT [Id], [Filemane], [Uploader] FROM sounds WHERE [Id] = @Id", con);
                soundCmd.Parameters.Add("@Id", SqlDbType.Int).SqlValue = id;
                SqlDataReader soundReader = soundCmd.ExecuteReader();
                if (soundReader.HasRows) {
                    while (soundReader.Read()) {
                        Id = soundReader.GetInt32(0);
                        Filename = soundReader.GetString(1);
                        Uploader = soundReader.GetString(2);
                    }
                }
                else {
                    return false; //No rows found
                }
                soundReader.Close();

                return true;
            }
        }

        public static List<Sound> LoadAll() {
            List<Sound> sounds = new List<Sound>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)) {
                con.Open();

                //get sound data, list of stalls
                SqlCommand soundCmd = new SqlCommand("SELECT [Id], [Filemane], [Uploader] FROM sounds", con);
                SqlDataReader soundReader = soundCmd.ExecuteReader();
                if (soundReader.HasRows) {
                    while (soundReader.Read()) {
                        Sound tempSound = new Sound (
                            soundReader.GetInt32(0),
                            soundReader.GetString(1),
                            soundReader.GetString(2)
                        );
                        sounds.Add(tempSound);
                    }
                }
                soundReader.Close();

                return sounds;
            }
        }
    }
}