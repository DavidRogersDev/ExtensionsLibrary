using System.Reflection;
using KesselRun.Extensions.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace KesselRun.Extensions.Tests
{
	[TestClass]
	public class StringExtensionsTest
	{
		private static string path;
		private const string ExpectedExceptionFail = "ExpectedException was not thrown.";
		private const string NotFound = "__NOT_FOUND__";
		private const string SampleSubstring = "datacentrenas";
		private const string PathParameter = "path";

		[ClassInitialize]
		public static void TestsSetup(TestContext context)
		{
			path = @"\\datacentrenas\MiscDisk2\Books I\Beginning CSS3\Beginning CSS3.pdf";
				   //01234567890123456789012345678901234567890123456789012345678901234567
		}

		[TestMethod]
		public void GetPartPathFromRightReturnsPartPath()
		{
			//  Arrange

			//  Act
			string result = path.GetPartPathFromRight(1, 2);

			//  Assert                        
			Assert.AreEqual(result, @"Books I");
		}

		[TestMethod]
		public void GetPartPathFromRightEdgeCaseFirstSlashReturnsPartPath()
		{
			//  Arrange

			//  Act
			string result = path.GetPartPathFromRight(0, 2);

			//  Assert                        
			Assert.AreEqual(result, @"Books I\Beginning CSS3");
		}

		[TestMethod]
		public void GetPartPathFromRightEdgeCaseLastSlashReturnsPartPath()
		{
			//  Arrange

			//  Act
			string result = path.GetPartPathFromRight(4, 5);

			//  Assert                        
			Assert.AreEqual(result, string.Empty);
		}

		[TestMethod]
		public void GetPartPathFromRightEdgeCaseLastSlashReturns2SlashesOfNetworkShare()
		{
			//  Arrange

			//  Act
			string result = path.GetPartPathFromRight(3, 5);

			//  Assert                        
			Assert.AreEqual(result, "\\datacentrenas");
		}
		
		[TestMethod]
		public void GetPartPathFromRightPassedSameIntForBothSlashes()
		{
			//  Arrange

			//  Act
			string result = path.GetPartPathFromRight(5, 5);

			//  Assert                        
			Assert.AreEqual(result, string.Empty);
		}

		[TestMethod]
		public void GetIndexOfNthCharReturnsNthChar()
		{
			//  Arrange

			//  Act
			int result = path.GetIndexOfNthChar(2, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(15, result);
		}

		[TestMethod]
		public void GetIndexOfNthCharReturnsNthCharEdgeCase1stChar()
		{
			//  Arrange

			//  Act
			int result = path.GetIndexOfNthChar(0, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void GetIndexOfNthCharReturnsNthCharEdgeCase2ndCharOfUnc()
		{
			//  Arrange

			//  Act
			int result = path.GetIndexOfNthChar(1, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void GetIndexOfNthCharReturnsNthCharEdgeCaseLastCharOfPath()
		{
			//  Arrange

			//  Act
			int result = path.GetIndexOfNthChar(5, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(48, result);
		}

		[TestMethod]
		public void GetIndexOfNthCharReturnsNthCharEdgeCaseLastCharIsLastCharOfPath()
		{

			//  Arrange
			string longPath = path + '\\';

			//  Act
			int result = longPath.GetIndexOfNthChar(6, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(67, result);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException),
			"You have requested the position of the Nth char where there are less than N instances of the char in the string."
			)]
		public void GetIndexOfNthCharThrowsExceptionInNthCharEdgeCaseWhereThereAreLessThanNInstances()
		{
			//  Arrange
			//  Act
		
			var result = path.GetIndexOfNthChar(6, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.Fail(ExpectedExceptionFail);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException),
			"The path parameter cannot be empty of composed only of white space.")]
		public void GetIndexOfNthCharThrowsExceptionWhereStringIsEmpty()
		{
			//  Arrange
			string emptyPathString = String.Empty;

			//  Act
			int result = emptyPathString.GetIndexOfNthChar(5, Path.DirectorySeparatorChar);

			//  Assert
			Assert.Fail(ExpectedExceptionFail);

		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException), "The path parameter cannot be null.")]
		public void GetIndexOfNthCharThrowsExceptionWhereStringIsNull()
		{
			//  Arrange
			string nullPathString = null;

			//  Act
			int result = nullPathString.GetIndexOfNthChar(5, Path.DirectorySeparatorChar);

			//  Assert
			Assert.Fail(ExpectedExceptionFail);
		}

		[TestMethod]
		public void GetIndexOfNthCharFromRightReturnsIndex()
		{
			//  Arrange

			//  Act
			int result = path.GetIndexOfNthCharFromRight(1, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(33, result);
		}

		[TestMethod]
		public void GetIndexOfNthCharFromRightEdgeCase1stChar()
		{
			//  Arrange

			//  Act
			int result = path.GetIndexOfNthCharFromRight(5, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void GetIndexOfNthCharFromRightEdgeCase1stCharIsLastCharOfPath()
		{
			//  Arrange
			string longPath = path + '\\';
			//  Act
			int result = longPath.GetIndexOfNthCharFromRight(0, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(67, result);
		}

		[TestMethod]
		public void GetIndexOfNthCharFromRightEdgeCase2ndCharOfUnc()
		{
			//  Arrange

			//  Act
			int result = path.GetIndexOfNthCharFromRight(4, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void GetIndexOfNthCharFromRightEdgeCaseLastCharOfPath()
		{
			//  Arrange

			//  Act
			int result = path.GetIndexOfNthCharFromRight(0, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.AreEqual(48, result);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException),
			"You have requested the position of the Nth char where there are less than N instances of the char in the string."
			)]
		public void GetIndexOfNthCharFromRightThrowsExceptionWhereThereAreLessThanNInstances()
		{
			//  Act
			int result = path.GetIndexOfNthCharFromRight(6, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.Fail(ExpectedExceptionFail);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException), "The path parameter cannot be null.")]
		public void GetIndexOfNthCharFromRightThrowsExceptionWhereStringIsNull()
		{
			//  Arrange
			string nullPathString = null;

			//  Act
			int result = nullPathString.GetIndexOfNthChar(1, Path.DirectorySeparatorChar);

			//  Assert                        
			Assert.Fail(ExpectedExceptionFail);
		}

		[TestMethod]
		public void SubstringBetweenNthAndMthReturnsCorrectString()
		{
			//  Arrange
			//  Act
			var actual = path.SubstringBetweenNthAndMth(25, 33);

			//  Assert            
			Assert.AreEqual("Books I", actual);

		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException), "The first index cannot be less than 0.")]
		public void SubstringBetweenNthAndMthWhen0orLessIsGivenForTheFirstChar()
		{
			//  Arrange
			//  Act
			var actual = path.SubstringBetweenNthAndMth(-5, 33);

			//  Assert            
			Assert.Fail(ExpectedExceptionFail);
				//   If it gets this far, the exception was not thrown and the test has failed.
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException), "The second index cannot be less than the first.")]
		public void SubstringBetweenNthAndMthWhenTheFirstCharIsLessThanTheSecondChar()
		{
			//  Arrange
			//  Act
			var actual = path.SubstringBetweenNthAndMth(5, 1);

			//  Assert            
			Assert.Fail(ExpectedExceptionFail);
				//   If it gets this far, the exception was not thrown and the test has failed.
		}

		[TestMethod]
		public void SubstringBetweenNthAndMthWhenTheSourceStringIsEmpty()
		{
			//  Arrange
			var path = string.Empty;

			//  Act
			var actual = path.SubstringBetweenNthAndMth(5, 20);

			//  Assert            
			Assert.AreEqual(NotFound, actual);
				//   If it gets this far, the exception was not thrown and the test has failed.
		}

		[TestMethod]
		public void SubstringBetweenNthAndMthWhenTheSourceStringIsNull()
		{
			//  Arrange
			string path = null;

			//  Act
			var actual = path.SubstringBetweenNthAndMth(5, 20);

			//  Assert            
			Assert.AreEqual(NotFound, actual);
				//   If it gets this far, the exception was not thrown and the test has failed.
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException), "Length cannot be less than zero.")]
		public void SubstringBetweenNthAndMthWhenNthAndMthAreSameInt()
		{
			//  Arrange

			//  Act
			var actual = path.SubstringBetweenNthAndMth(20, 20);

			//  Assert            
			Assert.Fail(ExpectedExceptionFail);
			//   If it gets this far, the exception was not thrown and the test has failed.
		}

		[TestMethod]
		public void GetPartPathFindsCorrectString()
		{
			//  Arrange

			//  Act
			var partPath = path.GetPartPath(2, 4);

			//  Assert            
			Assert.AreEqual(@"MiscDisk2\Books I", partPath);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException), "The second index cannot be less than the first.")]
		public void GetPartPathThrowsExceptionWhere2ndIndexGreaterThanFirst()
		{
			//  Arrange

			//  Act
			var partPath = path.GetPartPath(4, 2);

			//  Assert            
			Assert.Fail(ExpectedExceptionFail);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException),
			"Specified argument was out of the range of valid values.")]
		public void GetPartPathThrowsExceptionWhereParameterIndexesAreLargerThanHighestIndexOfString()
		{
			//  Arrange

			//  Act
			var partPath = path.GetPartPath(444, 445);

			//  Assert            
			Assert.Fail(ExpectedExceptionFail);
		}

		[TestMethod]
		public void GetPartPathPassedSameIntForBothSlashes()
		{
			//  Arrange

			//  Act
			var partPath = path.GetPartPath(3, 3);

			//  Assert            
			Assert.AreEqual(partPath, string.Empty);
		}

		[TestMethod]
		public void SubstringFromFindsStringToEnd()
		{
			//  Arrange

			//  Act
			var partPath = path.SubstringFrom("da");

			//  Assert            
			Assert.AreEqual(@"datacentrenas\MiscDisk2\Books I\Beginning CSS3\Beginning CSS3.pdf", partPath);
		}

		[TestMethod]
		public void SubstringFromFindsStringOfNChars()
		{
			//  Arrange

			//  Act
			var partPath = path.SubstringFrom("da", 13);

			//  Assert            
			Assert.AreEqual(SampleSubstring, partPath);
		}

		[TestMethod]
		public void SubstringFromDoesNotFindsStringAsCaseIsNotMatching()
		{
			//  Arrange

			//  Act
			var partPath = path.SubstringFrom("dA", 13);

			//  Assert            
			Assert.AreEqual(NotFound, partPath);
		}

		[TestMethod]
		public void SubstringFromWhereStringNotFound()
		{
			//  Arrange

			//  Act
			var partPath = path.SubstringFrom("NotInString");

			//  Assert            
			Assert.AreEqual(NotFound, partPath);
		}

		[TestMethod]
		public void SubstringFromNthToMthReturnsCorrectString()
		{
			//  Arrange

			//  Act
			var subString = path.SubstringFromNthToMth(2, 14);
			//  Assert                        
			Assert.AreEqual(SampleSubstring, subString);
		}

		[TestMethod]
		public void SubstringFromNthToMthWhereSourceIsEmpty()
		{
			//  Arrange
			string path = string.Empty;
			//  Act
			var subString = path.SubstringFromNthToMth(2, 14);
			//  Assert                        
			Assert.AreEqual(NotFound, subString, false);
		}

		[TestMethod]
		public void SubstringFromNthToMthWhereSourceIsNull()
		{
			//  Arrange
			string path = null;
			//  Act
			var subString = path.SubstringFromNthToMth(2, 14);
			//  Assert                        
			Assert.AreEqual(NotFound, subString, false);
		}


		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException),
			"Index and length must refer to a location within the string.")]
		public void SubstringFromNthToMthThrowsExceptionWhereUpperBoundExceedsHighestIndexOfString()
		{
			//  Arrange
			//  Act
			var subString = path.SubstringFromNthToMth(2, 140);
			//  Assert                        
			Assert.Fail(ExpectedExceptionFail);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException), "The second index cannot be less than the first")]
		public void SubstringFromNthToMthWhereSecondParamIsLessThanFirst()
		{
			//  Arrange
			//  Act
			var subString = path.SubstringFromNthToMth(14, 2);
			//  Assert                        
			Assert.Fail(ExpectedExceptionFail);
				//   If it gets this far, the exception was not thrown and the test has failed.
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException), "The first index cannot be less than 0")]
		public void SubstringFromNthToMthWhereFirstParamIsLessThan1()
		{
			//  Arrange
			//  Act
			var subString = path.SubstringFromNthToMth(-10, 2);
			//  Assert                        
			Assert.Fail(ExpectedExceptionFail);
				//   If it gets this far, the exception was not thrown and the test has failed.
		}

		[TestMethod]
		public void SubstringFromRightReturnsExpectedString()
		{
			//  Arrange

			//  Act
			var subStringFromRight = path.SubstringFromRight(3);

			//  Assert                        
			Assert.AreEqual("pdf", subStringFromRight);
		}

		[TestMethod]
		public void SubstringFromRightReturnsNotFoundWhereSourceStringIsEmpty()
		{
			//  Arrange
			string emptyString = string.Empty;
			//  Act
			var subStringFromRight = emptyString.SubstringFromRight(3);

			//  Assert                        
			Assert.AreEqual(NotFound, subStringFromRight);
		}

		[TestMethod]
		public void SubstringFromRightReturnsNotFoundWhereSourceStringIsNull()
		{
			//  Arrange
			string nullString = null;
			//  Act
			var subStringFromRight = nullString.SubstringFromRight(3);

			//  Assert                        
			Assert.AreEqual(NotFound, subStringFromRight);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentOutOfRangeException),
			"The string \"\\\\datacentrenas\\MiscDisk2\\Books I\\Beginning CSS3\\Beginning CSS3.pdf\" has a length which is less than 70 characters."
			)]
		public void SubstringFromRightParameterTooLarge()
		{
			//  Arrange

			//  Act
			var subStringFromRight = path.SubstringFromRight(70);

			//  Assert                        
			Assert.Fail(ExpectedExceptionFail);
				//   If it gets this far, the exception was not thrown and the test has failed.
		}

		[TestMethod]
		public void TryParseAsBoolParsesLegitimateBoolString()
		{
			//  Arrange
			string val = "true";

			//  Act
			var result = val.TryParseAsBool();

			//  Assert               
			Assert.IsTrue(result.HasValue);
			Assert.IsTrue(result.Value);
		}

		[TestMethod]
		public void TryParseAsBoolParsesIllegitimateBoolString()
		{
			//  Arrange
			string val = "wrong";

			//  Act
			var result = val.TryParseAsBool();

			//  Assert               
			Assert.IsFalse(result.HasValue);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TryParseAsBoolParsesEmptyString()
		{
			//  Arrange
			string val = string.Empty;

			//  Act
			var result = val.TryParseAsBool();

			//  Assert               
			Assert.IsFalse(result.HasValue);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TryParseAsBoolParsesNull()
		{
			//  Arrange
			string val = null;

			//  Act
			var result = val.TryParseAsBool();

			//  Assert               
			Assert.IsFalse(result.HasValue);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TryParseAsIntParsesLegitimateIntString()
		{
			//  Arrange
			string val = "4";

			//  Act
			var result = val.TryParseAsInt();

			//  Assert               
			Assert.IsTrue(result.HasValue);
			Assert.IsTrue(result.Value == 4);
		}

		[TestMethod]
		public void TryParseAsIntParsesLegitimateNegativeIntString()
		{
			//  Arrange
			string val = "-4";

			//  Act
			var result = val.TryParseAsInt();

			//  Assert               
			Assert.IsTrue(result.HasValue);
			Assert.IsTrue(result.Value == -4);
		}

		[TestMethod]
		public void TryParseAsIntParsesZeroString()
		{
			//  Arrange
			string zero = "0";

			//  Act
			var result = zero.TryParseAsInt();

			//  Assert               
			Assert.IsTrue(result.HasValue);
			Assert.IsTrue(result.Value == 0);
		}

		[TestMethod]
		public void TryParseAsIntParsesMaxIntString()
		{
			//  Arrange
			string intMax = int.MaxValue.ToString();

			//  Act
			var result = intMax.TryParseAsInt();

			//  Assert               
			Assert.IsTrue(result.HasValue);
			Assert.IsTrue(result.Value == int.MaxValue);
		}

		[TestMethod]
		public void TryParseAsIntReturnsNullOnMaxIntPlusOneString()
		{
			//  Arrange
			string bigNum = ((long) int.MaxValue + 1).ToString();

			//  Act
			var result = bigNum.TryParseAsInt();

			//  Assert               
			Assert.IsTrue(!result.HasValue);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TryParseAsIntReturnsNullForDecimalString()
		{
			//  Arrange
			string decimalNum = "10.9";

			//  Act
			var result = decimalNum.TryParseAsInt();

			//  Assert               
			Assert.IsTrue(!result.HasValue);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TryParseAsIntParsesIllegitimateintString()
		{
			//  Arrange
			string val = "wrong";

			//  Act
			var result = val.TryParseAsInt();

			//  Assert               
			Assert.IsFalse(result.HasValue);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TryParseAsIntParsesEmptyString()
		{
			//  Arrange
			string val = string.Empty;

			//  Act
			var result = val.TryParseAsInt();

			//  Assert               
			Assert.IsFalse(result.HasValue);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TryParseAsIntParsesNull()
		{
			//  Arrange
			string val = null;

			//  Act
			var result = val.TryParseAsInt();

			//  Assert               
			Assert.IsFalse(result.HasValue);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TrimSuffixFromEndTrimsSuffix()
		{
			//  Arrange
			var suffix = ".cshtml";
			var partial = "_login.cshtml";

			//  Act
			var result = partial.TrimSuffixFromEnd(suffix);

			//  Assert                        
			Assert.AreEqual("_login", result);
		}

		[TestMethod]
		public void TrimSuffixFromEndReturnsStringWhereSuffixNotFound()
		{
			//  Arrange
			var suffix = ".cshtml";
			var partial = "_login";

			//  Act
			var result = partial.TrimSuffixFromEnd(suffix);

			//  Assert                        
			Assert.AreEqual("_login", result);
		}

		[TestMethod]
		public void TrimSuffixFromEndReturnsStringWhereSuffixIsActuallyPrefix()
		{
			//  Arrange
			var suffix = ".cshtml";
			var partial = ".cshtml_login";

			//  Act
			var result = partial.TrimSuffixFromEnd(suffix);

			//  Assert                        
			Assert.AreEqual(".cshtml_login", result);
		}

		[TestMethod]
		public void TrimSuffixFromEndReturnsNullWhereSourceIsEmptyString()
		{
			//  Arrange
			var suffix = ".cshtml";
			var partial = string.Empty;

			//  Act
			var result = partial.TrimSuffixFromEnd(suffix);

			//  Assert                        
			Assert.AreEqual(string.Empty, result);
		}

		[TestMethod]
		public void TrimSuffixFromEndReturnsNullWhereSourceIsNull()
		{
			//  Arrange
			var suffix = ".cshtml";
			string partial = null;

			//  Act
			var result = partial.TrimSuffixFromEnd(suffix);

			//  Assert                        
			Assert.IsNull(result);
		}

		[TestMethod]
		public void ToDateTimeParsesDateAccordingToFormat()
		{
			var dateTarget = GetDate();

			//  Arrange
			const string format = "ddMMyyyy";
			const string dateString = "22112014";

			//  Act
			var result = dateString.ToDateTime(format);

			//  Assert                        
			Assert.IsInstanceOfType(result, typeof (DateTime));
			Assert.AreEqual(result, dateTarget);
		}

		[TestMethod]
		public void ToDateTimeParsesDateAccordingToFormatWithSlashes()
		{
			var dateTarget = GetDate();

			//  Arrange
			const string format = "dd/MM/yyyy";
			const string dateString = "22/11/2014";

			//  Act
			var result = dateString.ToDateTime(format);

			//  Assert                        
			Assert.IsInstanceOfType(result, typeof (DateTime));
			Assert.AreEqual(result, dateTarget);
		}

		[TestMethod]
		public void ToDateTimeParsesDateAccordingToFormatWithHyphens()
		{
			var dateTarget = GetDate();

			//  Arrange
			const string format = "dd-MM-yyyy";
			const string dateString = "22-11-2014";

			//  Act
			var result = dateString.ToDateTime(format);

			//  Assert                        
			Assert.IsInstanceOfType(result, typeof (DateTime));
			Assert.AreEqual(result, dateTarget);
		}

		[TestMethod]
		public void ToDateTimeParsesDateAccordingToFormatShortDate()
		{
			var dateTarget = GetDate();

			//  Arrange
			const string format = "yyMMdd";
			const string dateString = "141122";

			//  Act
			var result = dateString.ToDateTime(format);

			//  Assert                        
			Assert.IsInstanceOfType(result, typeof (DateTime));
			Assert.AreEqual(result, dateTarget);
		}

		[TestMethod]
		public void ToDateTimeFailsParseDateInWrongFormat()
		{
			var dateTarget = GetDate();

			//  Arrange
			const string format = "yyMMdd";
			const string dateString = "14-11-22";

			//  Act
            //  Assert                        
            ExceptionAssert.Throws<FormatException>(
                () => { var result = dateString.ToDateTime(format); }, 
                "String was not recognized as a valid DateTime."
                );

		}

		[TestMethod]
		public void GetPatternMatchesReturnsMatchesAsExpected()
		{
			//  Not much to test here, else we're testing the .NET frmwk itself, which has already been tested heaps.

			//  Arrange
			string stringToParse = "This is a string with words.";
			string pattern = "with";

			//  Act
			var result = stringToParse.GetPatternMatches(pattern);

			//  Assert                        
			Assert.IsTrue(result.Count == 1);
			CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Match));
		}

		[TestMethod]
		public void GetIndexesOfPatternMatchesReturnsIndexes()
		{
			//  Arrange
			string stringToParse = "this 4 string 7 has 5 integers -1";
			string pattern = @"\d";

			//  Act
			var indexes= stringToParse.GetIndexesOfPatternMatches(pattern);

			//  Assert                        
			Assert.AreEqual(indexes.Count, 4);
			Assert.AreEqual(indexes[1], 14);
		}

		[TestMethod]
		public void GetIndexesOfPatternMatchesReturnsNoIndexesWhenThereIsNoMatch()
		{
			//  Arrange
			string stringToParse = "this string has no integers.";
			string pattern = @"\d";

			//  Act
			var indexes= stringToParse.GetIndexesOfPatternMatches(pattern);

			//  Assert                        
			Assert.AreEqual(indexes.Count, 0);
		}

	    [TestMethod]
	    public void GetIndexOfNthCharHelperDoes()
	    {
            //  Arrange
	        var type = typeof (StringExtensions);
            var getIndexOfNthCharHelperMethod = type.GetMethod("GetIndexOfNthCharHelper", BindingFlags.NonPublic | BindingFlags.Static);

            //  Act
            int index = (int)getIndexOfNthCharHelperMethod.Invoke(null, new object[] { path, 2, 0, Path.DirectorySeparatorChar });

            //  Assert                        
            Assert.AreEqual(15, index);
	    }

		private static DateTime GetDate()
		{
			var dateTarget = new DateTime(2014, 11, 22);
			return dateTarget;
		}
	}
}
