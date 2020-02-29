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
using PF_API.Models;

namespace PF_API.Controllers
{
    public class ZapatosController : ApiController
    {
        private PFDBEntities db = new PFDBEntities();

        // GET: api/Zapatos
        public IQueryable<Zapatos> GetZapatos()
        {
            return db.Zapatos;
        }

        // GET: api/Zapatos/5
        [ResponseType(typeof(Zapatos))]
        public IQueryable<Zapatos> GetZapatos(string id)
        {
            return db.Zapatos.Where(d => d.Codigo == id);
        }


        // PUT: api/Zapatos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutZapatos(string id, Zapatos zapatos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zapatos.Codigo)
            {
                return BadRequest();
            }

            db.Entry(zapatos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZapatosExists(id))
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

        // POST: api/Zapatos
        [ResponseType(typeof(Zapatos))]
        public IHttpActionResult PostZapatos(Zapatos zapatos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Zapatos.Add(zapatos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ZapatosExists(zapatos.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = zapatos.Codigo }, zapatos);
        }

        // DELETE: api/Zapatos/5
        [ResponseType(typeof(Zapatos))]
        public IHttpActionResult DeleteZapatos(string id)
        {
            Zapatos zapatos = db.Zapatos.Find(id);
            if (zapatos == null)
            {
                return NotFound();
            }

            db.Zapatos.Remove(zapatos);
            db.SaveChanges();

            return Ok(zapatos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZapatosExists(string id)
        {
            return db.Zapatos.Count(e => e.Codigo == id) > 0;
        }
    }
}