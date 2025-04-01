using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace SIT.Components.Data {



    public static class Factory {

        private static Dictionary<string, IFactory> _factoryCache = new Dictionary<string, IFactory>();

        private static bool MatchConnectionType( ConnectionTypeAttribute attribute, string typeName ) {
            return attribute.TypeName.Equals( typeName, StringComparison.OrdinalIgnoreCase );
        }

        private static bool MatchConnectionType( Assembly asm, string typeName ) {
            ConnectionTypeAttribute attrib = GetConnectionTypeAttribute( asm );
            if( attrib == null )
                return false;
            return MatchConnectionType( attrib, typeName );
        }

        private static bool MatchConnectionType( System.Type type, string typeName ) {
            ConnectionTypeAttribute attrib = GetConnectionTypeAttribute( type );
            if( attrib == null )
                return false;
            return MatchConnectionType( attrib, typeName );
        }


        private static ConnectionTypeAttribute GetConnectionTypeAttribute( Assembly asm ) {
            object[] attribs = asm.GetCustomAttributes( typeof( ConnectionTypeAttribute ), true );
            if( attribs.Length > 0 )
                return attribs[ 0 ] as ConnectionTypeAttribute;
            return null;
        }

        private static ConnectionTypeAttribute GetConnectionTypeAttribute( System.Type type ) {
            object[] attribs = type.GetCustomAttributes( typeof( ConnectionTypeAttribute ), true );
            if( attribs.Length > 0 )
                return attribs[ 0 ] as ConnectionTypeAttribute;
            return null;
        }

        public static IFactory GetFactory( IDbConnection connection ) {
            string typeName = connection.GetType().FullName;
            if( _factoryCache.ContainsKey( typeName ) )
                return _factoryCache[ typeName ];

            //foreach( AssemblyName asmName in Assembly.GetEntryAssembly().GetReferencedAssemblies() ) {
            byte[] myPublicKeyToken = Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken();
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            foreach( FileInfo fi in di.GetFiles()) {
                AssemblyName asmName=null;
                try {
                    asmName = AssemblyName.GetAssemblyName( fi.FullName );

                } catch( BadImageFormatException ex ) {
                    //file is not an assembly
                    continue;
                } catch( Exception ex ) {
                    Trace.TraceError( "Fehler bei GetConnectionTypeAttribute (Datei: " + fi.FullName + "): " + ex.Message);
                    continue;
                }
                if( Array.Equals( asmName.GetPublicKeyToken(), myPublicKeyToken ) )
                    continue;

                Assembly asm = Assembly.Load( asmName.FullName );
                if( MatchConnectionType( asm, typeName ) ) {
                    System.Type factoryType=null;
                    foreach( Type t in asm.GetTypes() ){
                        if( MatchConnectionType( t, typeName ) ) {
                            factoryType=t;
                            break;

                        }
                    }
                    if( factoryType==null )
                        continue;

                    if( factoryType.GetInterface( typeof( IFactory ).FullName ) != null ) {
                        IFactory factory = Activator.CreateInstance( factoryType ) as IFactory;
                        if( !_factoryCache.ContainsKey( typeName ) )
                            _factoryCache.Add( typeName, factory );

                        return factory;


                    }
                }
            }
            return null;

        }



        public static IDbCommand CreateCommand( IDbConnection connection ) {
            return GetFactory( connection ).CreateCommand();
        }

        public static StoredProc CreateStoredProc( IDbConnection dbConnection, string procedureName ) {
            return GetFactory( dbConnection ).CreateStoredProc( dbConnection, procedureName );
        }

        public static StoredProc CreateStoredProc( IDbTransaction transaction, string procedureName ) {
            return GetFactory( transaction.Connection ).CreateStoredProc( transaction, procedureName );
        }

        public static StoredProc CreateStoredProc( DBConnection connection, string procedureName ) {
            return GetFactory( connection.Connection ).CreateStoredProc( connection.Connection, procedureName );
        }

        public static IDbDataAdapter CreateDataAdapter( IDbConnection connection ) {
            return GetFactory( connection ).CreateDataAdapter();
        }

        public static IDbConnection CreateConnection( IDbConnection connection ) {
            return GetFactory( connection ).CreateConnection();
        }

        public static IDbDataParameter CreateDataParameter( IDbConnection connection ) {
            return GetFactory( connection ).CreateDataParameter();
        }

        public static IDbUpdater CreateUpdater( IDbConnection connection ) {
            return GetFactory( connection ).CreateUpdater();
        }

        public static IBackupCreator CreateBackupCreator( IDbConnection connection ) {
            return GetFactory( connection ).CreateBackupCreator();
        }

        public static IBackupRecovery CreateBackupRecovery( IDbConnection connection ) {
            return GetFactory( connection ).CreateBackupRecovery();
        }

    }
}
