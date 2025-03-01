using Microsoft.AspNetCore.Mvc;
using CoreApp;   
using DTO;     
using System;
using System.Collections.Generic;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;

        
        public UserController()
        {
            _userManager = new UserManager(); 
        }

        // Método POST para crear un usuario
        [HttpPost]
        [Route("Create")]
        public ActionResult Create([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("El usuario no puede ser nulo.");
                }

                _userManager.Create(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var um = new UserManager();  
                var listResults = um.RetrieveAll();  
                return Ok(listResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update([FromBody] User user)
        {
            try
            {
                var um = new UserManager();
                um.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{userCode}")]
        public ActionResult Delete(string userCode)
        {
            try
            {
                var um = new UserManager();
                um.Delete(userCode);
                return Ok("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveById/{id}")]
        public ActionResult RetrieveById(int id)
        {
            try
            {
                var um = new UserManager();
                var user = um.RetrieveById(id);

                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByCode/{userCode}")]
        public ActionResult RetrieveUserByCode(string userCode)
        {
            try
            {
                var um = new UserManager();
                var user = um.RetrieveUserByCode(userCode);

                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
