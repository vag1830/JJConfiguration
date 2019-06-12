using System.Collections.Generic;
using Configuration.Cli;
using Configuration.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VariablesController : ControllerBase
	{
		private readonly VariableService _variablesService = new VariableService();

		[HttpGet]
		public ActionResult<IEnumerable<Variable>> GetAllVariables()
		{
			return Ok(_variablesService.GetAllVariables());
		}

		[HttpGet("{name}")]
		public ActionResult<Variable> GetVariableByName(string name)
		{
			var variable = _variablesService.GetVariableByName(name);

			return Ok(variable);
		}

		[HttpPost]
		public ActionResult<Variable> AddVariable([FromBody] Variable variable)
		{
			return Ok(_variablesService.AddVariable(variable));
		}

		[HttpPatch("{name}")]
		public ActionResult<Variable> AddVariableOverride(string name, [FromBody] KeyValuePair<string, string> keyValuePair)
		{
			var variable = VariableService.Variables.Find(var => var.Name == name);

			if (variable == null)
			{
				return NotFound();
			}

			//variable.Overrides.Add(keyValuePair);

			return Ok(variable);
		}

		[HttpDelete("{name}")]
		public ActionResult<Variable> DeleteVariable(string name)
		{
			var variable = VariableService.Variables.Find(v => v.Name == name);

			VariableService.Variables.Remove(variable);

			return variable;
		}
	}
}
