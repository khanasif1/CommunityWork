using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Domain.PropertyMatch.IService;
using Domain.PropertyMatch.Model;
using Domain.PropertyMatch.Service;
namespace Domain.PropertyMatch.UnitTest
{
    public class PropertyMatchTest
    {
        private IPropertyMatcher _propertyMatcher;
        private IRuleEngine _ruleEngine;
        #region Setup
        [SetUp]
        public void SetUp()
        {
            _propertyMatcher = Substitute.For<IPropertyMatcher>();
            _ruleEngine = Substitute.For<IRuleEngine>();
        }

        private AgencyFactory GetLREConcreteService()
        {
            return new LREConcrete();
        }
        private AgencyFactory GetCREConcreteService()
        {
            return new CREConcrete();
        }
        private AgencyFactory GetOTBREConcreteService()
        {
            return new OTBREConcrete();
        }

        private MatchRuleEngineFactory GetPunctuationRule()
        {
            return new PunctuationRuleConcrete();
        }
        private MatchRuleEngineFactory GetNameShuffleRule()
        {
            return new PropertyNameShuffleRuleConcrete();
        }
        private MatchRuleEngineFactory GetLocationRule()
        {
            return new LocationRuleConcrete();
        }
        List<Property> databaseProperties = PropertyDb.Instance.GetProperties(); 
        #endregion

        #region AgencyTest
        #region OTBRE Test
        /// <summary>
        /// Method will check for Name and Address match ignoring punctuations
        /// Result should return Valid match i.e. true
        /// </summary>
        [Test]
        public void InvokeMatch_Service_should_callFor_AgencyCode_OTBRE_With_MatchResult()
        {
            var service = GetOTBREConcreteService();
            MatchResult result = service.IsMatch(
                new Property { AgencyCode = "OTBRE", Name = "*Super*-High! APARTMENTS (Sydney)", Address = "32 Sir John-Young Crescent, Sydney, NSW." }, databaseProperties);
            Assert.IsTrue(result.IsValidMatch);
        }
        /// <summary>
        /// Method will check for Name and Address match ignoring punctuations
        /// Result should return Invalid match i.e. false. As  Name has "SS" added 
        /// </summary>
        [Test]
        public void InvokeMatch_Service_should_callFor_AgencyCode_OTBRE_With_UnMatchResult()
        {
            var service = GetOTBREConcreteService();
            MatchResult result = service.IsMatch(
                new Property { AgencyCode = "OTBRE", Name = "*SuperSS*-High! APARTMENTS (Sydney)", Address = "32 Sir John-Young Crescent, Sydney, NSW." }, databaseProperties);
            Assert.IsFalse(result.IsValidMatch);
        }
        #endregion
        #region CRE Test
        /// <summary>
        /// Method will check for Name ignoring sequence of words in name
        /// Result should return Valid match i.e. true
        /// </summary>
        [Test]
        public void InvokeMatch_Service_should_callFor_AgencyCode_CRE_With_MatchResult()
        {
            var service = GetCREConcreteService();
            MatchResult result = service.IsMatch(new Property { AgencyCode = "CRE", Name = "Apartments Summit The" }, databaseProperties);
            Assert.IsTrue(result.IsValidMatch);
        }
        /// <summary>
        /// Method will check for Name ignoring sequence of words in name
        /// Result should return Invalid match i.e. false. As  Name has "SS" added 
        /// </summary>
        [Test]
        public void InvokeMatch_Service_should_callFor_AgencyCode_CRE_With_UnMatchResult()
        {
            var service = GetCREConcreteService();
            MatchResult result = service.IsMatch(new Property { AgencyCode = "CRE", Name = "ApartmentsSS Summit The" }, databaseProperties);
            Assert.IsFalse(result.IsValidMatch);
        }

        #endregion
        #region LRE Test
        /// <summary>
        /// Test will check the distance between the Address and
        /// Co-ordiantes provided. Match will be succeed  as distance 
        /// is less than 200m
        /// </summary>
        [Test]
        public void InvokeMatch_Service_should_callFor_AgencyCode_LRE_With_MatchResult()
        {
            var service = GetLREConcreteService();
            MatchResult result = service.IsMatch(
                new Property { AgencyCode = "LRE", Address = "343 George St, Sydney NSW 2000", Latitude = -33.868402, Longitude = 151.207033 }, databaseProperties);
            Assert.IsTrue(result.IsValidMatch);
        }
        /// <summary>
        /// Test will check the distance between the Address and
        /// Co-ordiantes provided. Match will fail as distance 
        /// is greater than 200m
        /// </summary>
        [Test]
        public void InvokeMatch_Service_should_callFor_AgencyCode_LRE_With_UnMatchResult()
        {
            var service = GetLREConcreteService();
            MatchResult result = service.IsMatch(
                new Property { AgencyCode = "LRE", Address = "343 George St, Sydney NSW 2000", Latitude = -33.874010, Longitude = 151.207090 }, databaseProperties);
            Assert.IsFalse(result.IsValidMatch);
        }
        #endregion 
        #endregion

        #region RuleTest
        #region PunctuationTest
        [Test]
        /// <summary>
        /// Method will check punctuation rule
        /// </summary>
        public void InvokeRule_should_callFor_PunctuationRule_With_MatchResult()
        {
            var service = GetPunctuationRule();
            bool result = service.AbstractMatchPropertyDetails(
                new Property { AgencyCode = "OTBRE", Name = "*Super*-High! APARTMENTS (Sydney)", Address = "32 Sir John-Young Crescent, Sydney, NSW." }, databaseProperties);
            Assert.IsTrue(result);
        }  
        #endregion
        #region NameShuffleRuleTest
        [Test]
        /// <summary>
        /// Method will check NameShuffle match rule
        /// </summary>
        public void InvokeRule_should_callFor_NameShuffleRule_With_MatchResult()
        {
            var service = GetNameShuffleRule();
            bool result = service.AbstractMatchPropertyDetails(new Property { AgencyCode = "CRE", Name = "Apartments Summit The" }, databaseProperties);
            Assert.IsTrue(result);
        }
        #endregion
        #region LocationRuleTest
        [Test]
        /// <summary>
        /// Method will check Location match rule
        /// </summary>
        public void InvokeRule_should_callFor_LocationRule_With_MatchResult()
        {
            var service = GetLocationRule();
            bool result = service.AbstractMatchPropertyDetails(
                new Property { AgencyCode = "LRE", Address = "343 George St, Sydney NSW 2000", Latitude = -33.868402, Longitude = 151.207033 }, databaseProperties);
            Assert.IsTrue(result);
        }
        #endregion
        #endregion

    }
}
