using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DWProyecto.Models;

namespace DWProyecto.Controllers
{
    public class EMPLEADOController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/EMPLEADO
        public IQueryable<EMPLEADO> GetEMPLEADO()
        {
            return db.EMPLEADO;
        }

        // GET: api/EMPLEADO/5
        [ResponseType(typeof(EMPLEADO))]
        public IHttpActionResult GetEMPLEADO(int id)
        {
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return NotFound();
            }

            return Ok(eMPLEADO);
        }

        // PUT: api/EMPLEADO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMPLEADO(int id, EMPLEADO eMPLEADO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMPLEADO.EMP_ID)
            {
                return BadRequest();
            }

            db.Entry(eMPLEADO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMPLEADOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EMPLEADO
        [ResponseType(typeof(EMPLEADO))]
        public IHttpActionResult PostEMPLEADO(EMPLEADO eMPLEADO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMPLEADO.Add(eMPLEADO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eMPLEADO.EMP_ID }, eMPLEADO);
        }

        // DELETE: api/EMPLEADO/5
        [ResponseType(typeof(EMPLEADO))]
        public IHttpActionResult DeleteEMPLEADO(int id)
        {
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return NotFound();
            }

            db.EMPLEADO.Remove(eMPLEADO);
            db.SaveChanges();

            return Ok(eMPLEADO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMPLEADOExists(int id)
        {
            return db.EMPLEADO.Count(e => e.EMP_ID == id) > 0;
        }
    }
}