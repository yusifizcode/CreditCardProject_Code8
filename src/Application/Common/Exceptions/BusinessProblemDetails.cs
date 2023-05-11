using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TechnestHackhaton.Application.Exceptions;

public class BusinessProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);

}