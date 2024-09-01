using Practica01.datos.interfaces;
using Practica01.datos.repositorios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        private IFactura _repositorioFactura;
        private IDetalleFactura _repositorioDetalleFactura;

        public IFactura RepositorioFactura { 
            get 
            {
                if( _repositorioFactura == null)
                {
                    _repositorioFactura = new RepositorioFactura();
                }
                return _repositorioFactura;
            }
        }
        public IDetalleFactura RepositorioDetalleFactura
        {
            get
            {
                if (_repositorioDetalleFactura == null)
                {
                    _repositorioDetalleFactura = new RepositorioDetalleFactura();
                }
                return _repositorioDetalleFactura;
            }
        }
        public UnitOfWork(string cnnString) 
        {
            _connection = new SqlConnection(cnnString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void GuardarCambios()
        {
            try
            {
                _transaction.Commit();
            }catch(Exception ex)
            {
                _transaction.Rollback();
                throw new Exception($"Error al guardar cambios. Error: {ex.Message}");
            }
        }

        public void Dispose()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
            }
            if(_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
