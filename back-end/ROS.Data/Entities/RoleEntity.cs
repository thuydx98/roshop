using Microsoft.AspNetCore.Identity;
using ROS.Contracts.Entities;
using System;

namespace ROS.Data.Entities
{
	public class RoleEntity : IdentityRole<int>, ICreatedEntity, IUpdatedEntity
	{
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }
	}
}
