//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Data;
using System.Collections;

namespace SIT.Components.Data {
    /// <summary>
    /// Zusammenfassung für Class1.
    /// </summary>
    public class StoredProcedure {

        public static IDbCommand Run( IDbConnection dbcon, string procname, IDbDataParameter[] parameters ) {

            IDbCommand cmd = dbcon.CreateCommand();
            cmd.CommandText = procname;
            cmd.CommandType = CommandType.StoredProcedure;
            return AddParameters( cmd, parameters );

        }

        public static IDbCommand Run( IDbConnection dbcon, string procname, IDbDataParameter[] inparameters, IDbDataParameter[] outparameters ) {
            return AddParameters( Run( dbcon, procname, inparameters ), outparameters );
        }

        public static IDbCommand AddParameters( IDbCommand cmd, IDbDataParameter[] parameters ) {
            foreach( IDbDataParameter param in parameters )
                cmd.Parameters.Add( param );
            return cmd;
        }


        public static IDbCommand Run( IDbConnection dbcon, string procname, string parameters ) {


            string[] sparams;
            int idx;
            ArrayList paras;
            IDbDataParameter para;

            IDbCommand cmd = dbcon.CreateCommand();
            paras = new ArrayList();

            sparams = parameters.Split( ',' );
            foreach( string sparam in sparams ) {

                idx = sparam.IndexOf( '=' );

                para = cmd.CreateParameter();
                para.ParameterName = sparam.Substring( 0, idx ).Replace( " ", "");
                para.Value = sparam.Substring( idx + 1 );
                paras.Add( para );

            }

            para = null;
            sparams = null;
            cmd.Dispose();

            return Run( dbcon, procname, paras.ToArray( typeof( IDbDataParameter ) ) as IDbDataParameter[] );

        }

        public static IDbCommand Run( IDbConnection dbcon, string procname, string inparameters, IDbDataParameter[] outparameters ) {

            IDbCommand cmd = Run( dbcon, procname, inparameters );
            return AddParameters( cmd, outparameters );

        }

        public static object EcexuteScalar( SIT.Components.Data.DBConnection dbcon, string procname, string parameters ) {

            IDbConnection tmpdbcon = null;
            IDbCommand cmd = null;
            object retval;

            try {
                tmpdbcon = dbcon.Duplicate();
                tmpdbcon.Open();

                cmd = Run(
                    tmpdbcon,
                    procname,
                    parameters                    
                    );

                retval = cmd.ExecuteScalar();
                return retval;
            } catch {
                throw;
            } finally {
                if( cmd != null )
                    cmd.Dispose();
                if( tmpdbcon != null ) {
                    tmpdbcon.Close();
                    tmpdbcon.Dispose();
                }
            }

        }

        public static void EcexuteNonQuery( SIT.Components.Data.DBConnection dbcon, string procname, string parameters ) {

            IDbConnection tmpdbcon = null;
            IDbCommand cmd = null;

            try {

                tmpdbcon = dbcon.Duplicate();
                tmpdbcon.Open();

                cmd = Run(
                    tmpdbcon,
                    procname,
                    parameters
                    );

                cmd.ExecuteNonQuery();
            } catch {
                throw;
            } finally {
                if( cmd != null )
                    cmd.Dispose();
                if( tmpdbcon != null ) {
                    tmpdbcon.Close();
                    tmpdbcon.Dispose();
                }
            }

        }

        public static void EcexuteNonQuery( SIT.Components.Data.DBConnection dbcon, string procname, string inparameters, IDbDataParameter[] outparameters ) {

            IDbConnection tmpdbcon = null;
            IDbCommand cmd = null;

            try {

                tmpdbcon = dbcon.Duplicate();
                tmpdbcon.Open();

                cmd = Run(
                    tmpdbcon,
                    procname,
                    inparameters,
                    outparameters
                );

                cmd.ExecuteNonQuery();
            } catch {
                throw;
            } finally {
                if( cmd != null )
                    cmd.Dispose();
                if( tmpdbcon != null ) {
                    tmpdbcon.Close();
                    tmpdbcon.Dispose();
                }
            }

        }

        public static IDataReader ExecuteReader( SIT.Components.Data.DBConnection dbcon, string procname, string parameters, CommandBehavior behavior ) {

            IDbConnection tmpdbcon = null;
            IDataReader dr = null;
            IDbCommand cmd = null;

            try {
                tmpdbcon = dbcon.Duplicate();
                tmpdbcon.Open();

                cmd = Run(
                    tmpdbcon,
                    procname,
                    parameters
                    );

                dr = cmd.ExecuteReader( behavior );
                return dr;
            } catch {
                throw;
            } finally {
                if( cmd != null )
                    cmd.Dispose();
            }

        }

        public static IDataReader ExecuteReader( SIT.Components.Data.DBConnection dbcon, string procname, string parameters ) {
            return ExecuteReader( dbcon, procname, parameters, CommandBehavior.Default );
        }

    }
}

