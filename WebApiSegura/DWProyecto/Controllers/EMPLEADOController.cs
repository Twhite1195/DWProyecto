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
    [Authorize]
    [RoutePrefix("api/EMPLEADO")]
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
            EMPLEADO empleado = db.EMPLEADO.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // PUT: api/EMPLEADO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMPLEADO(int id, EMPLEADO empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empleado.EMP_ID)
            {
                return BadRequest();
            }

            db.Entry(empleado).State = EntityState.Modified;

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
        public IHttpActionResult PostEMPLEADO(EMPLEADO empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMPLEADO.Add(empleado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = empleado.EMP_ID }, empleado);
        }

        // DELETE: api/EMPLEADO/5
        [ResponseType(typeof(EMPLEADO))]
        public IHttpActionResult DeleteEMPLEADO(int id)
        {
            EMPLEADO empleado = db.EMPLEADO.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            db.EMPLEADO.Remove(empleado);
            db.SaveChanges();

            return Ok(empleado);
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