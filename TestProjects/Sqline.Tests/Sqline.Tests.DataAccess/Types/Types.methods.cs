using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Sqline.ClientFramework;
using Sqline.Tests.DataAccess.ViewItems;
namespace Sqline.Tests.DataAccess {

	public partial class TypesHandler {

		public long GetBigInt() {
			long OResult = default(long);
			string OSql = @"SELECT BigIntColumn FROM TypeTest WHERE BigIntColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							long OScalarItem = OReader.GetInt64(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public byte[] GetBinary() {
			byte[] OResult = default(byte[]);
			string OSql = @"SELECT BinaryColumn FROM TypeTest WHERE BinaryColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							byte[] OScalarItem = OReader.GetBytes(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public bool GetBit() {
			bool OResult = default(bool);
			string OSql = @"SELECT BitColumn FROM TypeTest WHERE BitColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							bool OScalarItem = OReader.GetBoolean(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public string GetChar() {
			string OResult = default(string);
			string OSql = @"SELECT CharColumn FROM TypeTest WHERE CharColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							string OScalarItem = OReader.GetString(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public DateTime GetDate() {
			DateTime OResult = default(DateTime);
			string OSql = @"SELECT DateColumn FROM TypeTest WHERE DateColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							DateTime OScalarItem = OReader.GetDateTime(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public DateTime GetDateTime() {
			DateTime OResult = default(DateTime);
			string OSql = @"SELECT DateTimeColumn FROM TypeTest WHERE DateTimeColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							DateTime OScalarItem = OReader.GetDateTime(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public DateTime GetDateTime2() {
			DateTime OResult = default(DateTime);
			string OSql = @"SELECT DateTime2Column FROM TypeTest WHERE DateTime2Column IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							DateTime OScalarItem = OReader.GetDateTime(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public DateTimeOffset GetDateTimeOffset() {
			DateTimeOffset OResult = default(DateTimeOffset);
			string OSql = @"SELECT DateTimeOffsetColumn FROM TypeTest WHERE DateTimeOffsetColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							DateTimeOffset OScalarItem = OReader.GetDateTimeOffset(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public decimal GetDecimal() {
			decimal OResult = default(decimal);
			string OSql = @"SELECT DecimalColumn FROM TypeTest WHERE DecimalColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							decimal OScalarItem = OReader.GetDecimal(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public byte[] GetFileStream() {
			byte[] OResult = default(byte[]);
			string OSql = @"SELECT FileStreamColumn FROM TypeTest WHERE FileStreamColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							byte[] OScalarItem = OReader.GetBytes(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public double GetFloat() {
			double OResult = default(double);
			string OSql = @"SELECT FloatColumn FROM TypeTest WHERE FloatColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							double OScalarItem = OReader.GetDouble(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public byte[] GetImage() {
			byte[] OResult = default(byte[]);
			string OSql = @"SELECT ImageColumn FROM TypeTest WHERE ImageColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							byte[] OScalarItem = OReader.GetBytes(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public int GetInt() {
			int OResult = default(int);
			string OSql = @"SELECT IntColumn FROM TypeTest WHERE IntColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							int OScalarItem = OReader.GetInt32(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public decimal GetMoney() {
			decimal OResult = default(decimal);
			string OSql = @"SELECT MoneyColumn FROM TypeTest WHERE MoneyColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							decimal OScalarItem = OReader.GetDecimal(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public string GetNChar() {
			string OResult = default(string);
			string OSql = @"SELECT NCharColumn FROM TypeTest WHERE NCharColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							string OScalarItem = OReader.GetString(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public string GetNText() {
			string OResult = default(string);
			string OSql = @"SELECT NTextColumn FROM TypeTest WHERE NTextColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							string OScalarItem = OReader.GetString(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public decimal GetNumeric() {
			decimal OResult = default(decimal);
			string OSql = @"SELECT NumericColumn FROM TypeTest WHERE NumericColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							decimal OScalarItem = OReader.GetDecimal(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public string GetNVarChar() {
			string OResult = default(string);
			string OSql = @"SELECT NVarCharColumn FROM TypeTest WHERE NVarCharColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							string OScalarItem = OReader.GetString(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public float GetReal() {
			float OResult = default(float);
			string OSql = @"SELECT RealColumn FROM TypeTest WHERE RealColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							float OScalarItem = OReader.GetFloat(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public DateTime GetSmallDateTime() {
			DateTime OResult = default(DateTime);
			string OSql = @"SELECT SmallDateTimeColumn FROM TypeTest WHERE SmallDateTimeColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							DateTime OScalarItem = OReader.GetDateTime(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public short GetSmallInt() {
			short OResult = default(short);
			string OSql = @"SELECT SmallIntColumn FROM TypeTest WHERE SmallIntColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							short OScalarItem = OReader.GetInt16(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public decimal GetSmallMoney() {
			decimal OResult = default(decimal);
			string OSql = @"SELECT SmallMoneyColumn FROM TypeTest WHERE SmallMoneyColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							decimal OScalarItem = OReader.GetDecimal(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public string GetText() {
			string OResult = default(string);
			string OSql = @"SELECT TextColumn FROM TypeTest WHERE TextColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							string OScalarItem = OReader.GetString(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public TimeSpan GetTime() {
			TimeSpan OResult = default(TimeSpan);
			string OSql = @"SELECT TimeColumn FROM TypeTest WHERE TimeColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							TimeSpan OScalarItem = OReader.GetTime(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public byte[] GetTimestamp() {
			byte[] OResult = default(byte[]);
			string OSql = @"SELECT TimestampColumn FROM TypeTest WHERE TimestampColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							byte[] OScalarItem = OReader.GetBytes(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public byte GetTinyInt() {
			byte OResult = default(byte);
			string OSql = @"SELECT TinyIntColumn FROM TypeTest WHERE TinyIntColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							byte OScalarItem = OReader.GetByte(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public Guid GetUniqueIdentifier() {
			Guid OResult = default(Guid);
			string OSql = @"SELECT UniqueIdentifierColumn FROM TypeTest WHERE UniqueIdentifierColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							Guid OScalarItem = OReader.GetGuid(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public byte[] GetVarBinary() {
			byte[] OResult = default(byte[]);
			string OSql = @"SELECT VarBinaryColumn FROM TypeTest WHERE VarBinaryColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							byte[] OScalarItem = OReader.GetBytes(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public string GetVarChar() {
			string OResult = default(string);
			string OSql = @"SELECT VarCharColumn FROM TypeTest WHERE VarCharColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							string OScalarItem = OReader.GetString(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
		public string GetXml() {
			string OResult = default(string);
			string OSql = @"SELECT XmlColumn FROM TypeTest WHERE XmlColumn IS NOT NULL";
			using (IDbConnection OConnection = DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							string OScalarItem = OReader.GetString(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
	}
}
