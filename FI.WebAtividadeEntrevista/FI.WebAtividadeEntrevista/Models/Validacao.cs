using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebAtividadeEntrevista.Models
{
	public static class ValidaCPF
	{
		private static string formule = "{11144477735}  (N.Count == 11) and   N[9] == (((N[0] * 10 + N[1] * 9 + N[2] * 8 + N[3] * 7 + N[4] * 6 + N[5] * 5 + N[6] * 4 + N[7] * 3 + N[8] * 2) % 11) < 2 ? 0 : 11 - ((N[0] * 10 + N[1] * 9 + N[2] * 8 + N[3] * 7 + N[4] * 6 + N[5] * 5 + N[6] * 4 + N[7] * 3 + N[8] * 2) % 11)) and  N[10] == (((N[0] * 11 + N[1] * 10 + N[2] * 9 + N[3] * 8 + N[4] * 7 + N[5] * 6 + N[6] * 5 + N[7] * 4 + N[8] * 3 + N[9] * 2) % 11) < 2 ? 0 : 11 - ((N[0] * 11 + N[1] * 10 + N[2] * 9 + N[3] * 8 + N[4] * 7 + N[5] * 6 + N[6] * 5 + N[7] * 4 + N[8] * 3 + (((N[0] * 10 + N[1] * 9 + N[2] * 8 + N[3] * 7 + N[4] * 6 + N[5] * 5 + N[6] * 4 + N[7] * 3 + N[8] * 2) % 11) < 2 ? 0 : 11 - ((N[0] * 10 + N[1] * 9 + N[2] * 8 + N[3] * 7 + N[4] * 6 + N[5] * 5 + N[6] * 4 + N[7] * 3 + N[8] * 2) % 11)) * 2) % 11))";
		public static bool IsValid(this string numberString)
		{
			if (string.IsNullOrEmpty(formule)) return true;
			var exp = GetExpressionString(formule);
			var listInt = new List<int>();
			var listChar = new List<char>();
			foreach (var c in numberString.ToCharArray())
			{
				var number = 0;
				var isNumeric = Int32.TryParse(c.ToString(CultureInfo.InvariantCulture), out number);
				listInt.Add(isNumeric ? number : c);
				listChar.Add(c);
			}
			try
			{
				var paramListInt = Expression.Parameter(typeof(List<int>), "N");
				var paramListChar = Expression.Parameter(typeof(List<char>), "C");
				var lambdaExp = System.Linq.Dynamic.DynamicExpression.ParseLambda(new[] { paramListInt, paramListChar }, null, exp);
				var result = lambdaExp.Compile().DynamicInvoke(listInt, listChar);
				return (bool)result;
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message);
			}

		}

		private static string GetExpressionString(string formula)
		{
			var beginIndex = formula.IndexOf('{');
			var endIndex = formula.IndexOf('}');
			var numSample = formula.Substring(beginIndex, (endIndex - beginIndex) + 1);
			var exp = formula.Replace(numSample, string.Empty);
			return exp;
		}
	}
}
