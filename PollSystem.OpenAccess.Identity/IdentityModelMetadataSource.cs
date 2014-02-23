#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the FluentMappingGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Telerik.OpenAccess.Metadata.Relational;

namespace PollSystem.OpenAccess.Identity
{

	public partial class IdentityModelMetadataSource : FluentMetadataSource
	{
		
		protected override IList<MappingConfiguration> PrepareMapping()
		{
			List<MappingConfiguration> mappingConfigurations = new List<MappingConfiguration>();
            mappingConfigurations.Add(this.CraeteIdentityUserMapping());
            mappingConfigurations.Add(this.CreateIdentityRoleMapping());
            mappingConfigurations.Add(this.CreateQuestionMapping());
            mappingConfigurations.Add(this.CraeteAnswerConfiguration());
			
			return mappingConfigurations;
		}

        private MappingConfiguration<IdentityUser> CraeteIdentityUserMapping()
        {
            MappingConfiguration<IdentityUser> identityUserMapping = new MappingConfiguration<IdentityUser>();
            identityUserMapping.MapType(user => new 
            {
                Id = user.Id,
                Name = user.UserName, 
                PasswordHash = user.PasswordHash
            }).ToTable("IdentityUsers");
            identityUserMapping.HasProperty(x => x.Id).IsIdentity();

            return identityUserMapping;
        }

        private MappingConfiguration<IdentityRole> CreateIdentityRoleMapping()
        {
            MappingConfiguration<IdentityRole> identityRoleMapping = new MappingConfiguration<IdentityRole>();
            identityRoleMapping.MapType(role => new 
            {
                id = role.Id,
                Name = role.Name
            }).ToTable("IdentityRoles");
            identityRoleMapping.HasProperty(x => x.Id).IsIdentity();

            return identityRoleMapping;
        }

        private MappingConfiguration<Question> CreateQuestionMapping()
        {
            MappingConfiguration<Question> questionMapping = new MappingConfiguration<Question>();
            questionMapping.MapType(question => new
            {
                QuestionId = question.Id,
                QueryText = question.QueryText,
                PostedById = question.PostedById
            })
            .ToTable("Questions");
            questionMapping.HasProperty(q => q.Id).IsIdentity();
            questionMapping.HasAssociation(q => q.PostedBy)
                .WithOpposite(u => u.CreatedQuestions)
                .HasConstraint((q, u) => q.PostedById == u.Id);
            questionMapping.HasAssociation(q => q.UsersThatVoted)
                .WithOpposite(u => u.VotedOnQuestions)
                .IsManaged()
                .MapJoinTable("QuestionsUsers", (q, u) => new 
                    {
                        QuestionGuid = q.Id,
                        UserGuid = u.Id
                    });
            questionMapping.HasAssociation(q => q.Answers)
                .WithOpposite(a => a.Question)
                .HasConstraint((q, a) => q.Id == a.QuestionId)
                .IsManaged();

            return questionMapping;
        }

        private MappingConfiguration<Answer> CraeteAnswerConfiguration()
        {
            MappingConfiguration<Answer> answerMapping = new MappingConfiguration<Answer>();
            answerMapping.MapType(answer => new
            {
                AnswerId = answer.Id,
                AnwerText = answer.AnswerText,
                QuestionId = answer.QuestionId, 
                Votes = answer.Votes
            })
            .ToTable("Answers");
            answerMapping.HasProperty(x => x.Id).IsIdentity();
            
            return answerMapping;
        }

		protected override void SetContainerSettings(MetadataContainer container)
		{
			container.Name = "IdentityModel";
			container.DefaultNamespace = "PollSystem.OpenAccess.Identity";
			container.NameGenerator.SourceStrategy = Telerik.OpenAccess.Metadata.NamingSourceStrategy.Property;
			container.NameGenerator.RemoveCamelCase = false;
		}

	}
}
#pragma warning restore 1591