using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XmlExplorer.Controls
{
    public class RegularExpressions
    {
        /// <summary>
        ///  Regular expression built for C# on: Fri, Jul 20, 2007, 10:46:24 PM
        ///  Using Expresso Version: 3.0.2745, http://www.ultrapico.com
        ///  
        ///  A description of the regular expression:
        ///  
        ///  [OpenTagStart]: A named capture group. [<!--]
        ///      <!--
        ///          <!--
        ///  Whitespace, any number of repetitions
        ///  [Comments]: A named capture group. [.*]
        ///      Any character, any number of repetitions
        ///  Whitespace, any number of repetitions
        ///  [OpenTagEnd]: A named capture group. [-->]
        ///      -->
        ///          -->
        ///  
        ///
        /// </summary>
        public static Regex Comments = new Regex(
              @"(?<Delimiter><!--)\s*(?<Comments>.*)\s*(?<Delimiter>-->)",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        /// <summary>
        ///  Regular expression built for C# on: Fri, Jul 20, 2007, 10:51:05 PM
        ///  Using Expresso Version: 3.0.2745, http://www.ultrapico.com
        ///  
        ///  A description of the regular expression:
        ///  
        ///  [OpenTagStart]: A named capture group. [<|<\?|</]
        ///      Select from 3 alternatives
        ///          <
        ///          <\?
        ///              <
        ///              Literal ?
        ///          </
        ///              </
        ///  Whitespace, any number of repetitions
        ///  [OpenTagName]: A named capture group. [\w+]
        ///      Alphanumeric, one or more repetitions
        ///  Whitespace, any number of repetitions
        ///  [Attribute]: A named capture group. [(?<AttributeName>\w+)(?<Equals>=)(?<Quote>")(?<AttributeValue>\w*)(?<Quote>")], any number of repetitions
        ///      (?<AttributeName>\w+)(?<Equals>=)(?<Quote>")(?<AttributeValue>\w*)(?<Quote>")
        ///          [AttributeName]: A named capture group. [\w+]
        ///              Alphanumeric, one or more repetitions
        ///          [Equals]: A named capture group. [=]
        ///              =
        ///          [Quote]: A named capture group. ["]
        ///              "
        ///          [AttributeValue]: A named capture group. [\w*]
        ///              Alphanumeric, any number of repetitions
        ///          [Quote]: A named capture group. ["]
        ///              "
        ///  Whitespace, any number of repetitions
        ///  [OpenTagEnd]: A named capture group. [>|/>|\?>]
        ///      Select from 3 alternatives
        ///          >
        ///          />
        ///              />
        ///          \?>
        ///              Literal ?
        ///              >
        ///  
        ///
        /// </summary>
        public static Regex Xml = new Regex(
              @"(?<Delimiter></?|<\?|<!--)\s*(?<Name>\w+)\s*(?<Attribute>(?<AttributeName>\S+)(?<Delimiter>=)(?<Text>"")(?<AttributeValue>.*?)(?<Text>"")\s*)*?(?<Delimiter>/?>|\?>|-->)",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        //// Replace the matched text in the InputText using the replacement pattern
        // string result = regex.Replace(InputText,regexReplace);

        //// Split the InputText wherever the regex matches
        // string[] results = regex.Split(InputText);

        //// Capture the first Match, if any, in the InputText
        // Match m = regex.Match(InputText);

        //// Capture all Matches in the InputText
        // MatchCollection ms = regex.Matches(InputText);

        //// Test to see if there is a match in the InputText
        // bool IsMatch = regex.IsMatch(InputText);

        //// Get the names of all the named and numbered capture groups
        // string[] GroupNames = regex.GetGroupNames();

        //// Get the numbers of all the named and numbered capture groups
        // int[] GroupNumbers = regex.GetGroupNumbers();
    }
}
