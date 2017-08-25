using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace MDW.Tools
{
    public class Serializer
    {
        #region Enum

        public enum Format
        {
            Xml,
            Json
        }

        #endregion
    }

    public class Serializer<T>
    {
        #region Singleton

        public static Serializer<T> Current { get { return new Serializer<T>(); } }

        #endregion

        #region .ctor

        private Serializer() { }

        #endregion

        #region Serializer

        public string Serialize(T o)
        {
            string result = string.Empty;

            try
            {
                if (o is Exception)
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        bin.Serialize(stream, o);

                        result = Convert.ToBase64String(stream.ToArray());
                    }
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    using (MemoryStream stream = new MemoryStream())
                    {
                        if (stream.CanWrite)
                        {
                            using (StreamWriter writer = new StreamWriter(stream))
                            {
                                serializer.Serialize(writer.BaseStream, o);

                                if (stream.CanSeek)
                                    stream.Position = 0;

                                if (stream.CanRead)
                                {
                                    using (StreamReader reader = new StreamReader(stream))
                                        result = reader.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        public string Serialize(T o, Serializer.Format format)
        {
            string result = string.Empty;

            try
            {
                if (format == Serializer.Format.Xml)
                    result = Serialize(o);

                if (format == Serializer.Format.Json)
                    result = new JavaScriptSerializer().Serialize(o);
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region Deserializer

        public T Deserialize(string s, Encoding e)
        {
            T result = default(T);

            try
            {
                XmlSerializer o = new XmlSerializer(typeof(T));

                using (MemoryStream stream = new MemoryStream(e.GetBytes(s)))
                    result = (T)o.Deserialize(stream);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        public T Deserialize(string s, Serializer.Format format)
        {
            T result = default(T);

            try
            {
                if (format == Serializer.Format.Xml)
                    result = Deserialize(s);

                if (format == Serializer.Format.Json)
                    result = new JavaScriptSerializer().Deserialize<T>(s);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        public T Deserialize(string s)
        {
            T result = default(T);

            try
            {
                if (typeof(T) == typeof(Exception))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(s)))
                        result = (T)bin.Deserialize(stream);
                }
                else
                {
                    XmlSerializer o = new XmlSerializer(typeof(T));

                    using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(s)))
                        result = (T)o.Deserialize(stream);
                }
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        public T Deserialize(byte[] data)
        {
            T result = default(T);

            try
            {
                XmlSerializer o = new XmlSerializer(typeof(T));

                using (MemoryStream stream = new MemoryStream(data))
                    result = (T)o.Deserialize(stream);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        #endregion
    }
}