using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NsavinCodesCounter
{
	class ScanParameters
	{
		private static ScanParameters scanParameters;
		private static ScanParameters TEXT;

		private static Dictionary<string, ScanParameters> extenstionToNotation = initETNDictionary();

		private static Dictionary<string, ScanParameters> initETNDictionary()
		{
			Dictionary<string, ScanParameters> res = new Dictionary<string, ScanParameters>();

			scanParameters = new ScanParameters();
			scanParameters.setUselessChars(new char[] { '\t', ' ', '\n', '{', '}', '(', ')', ';', '\r' });
			scanParameters.setCommentRegex("(/\\*[^\\*]*[^/]*/|\\/\\/[^\\n]*)");

			TEXT = new ScanParameters();
			TEXT.setUselessChars(new char[]{});

			res.Add("*", TEXT);

			res.Add(".java", scanParameters);
			res.Add(".cs", scanParameters);
			res.Add(".as", scanParameters);
			res.Add(".cpp", scanParameters);
			res.Add(".h", scanParameters);
			res.Add(".c", scanParameters);



			return res;
		}

		public static ScanParameters getScanParametersForExtension(string ext) {
			ScanParameters p;
			if(extenstionToNotation.ContainsKey(ext)){
				p = extenstionToNotation[ext];
			}else{
				p = extenstionToNotation["*"];
			}
			return p;
		}

		private char[] uselessSymbols;
		private Regex commentRegExp;

		public void setUselessChars(char[] chars) {
			uselessSymbols = chars;
		}

		public void setCommentRegex(string regexString) {
			commentRegExp = new Regex(regexString);
		}

		public char[] getUselessChars() {
			return uselessSymbols;
		}

		public Regex getCommentRegex() {
			return commentRegExp;
		}




	}
}
