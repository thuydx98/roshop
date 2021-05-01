using Microsoft.AspNetCore.Identity;
using ROS.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace ROS.Data.Entities
{
	public partial class UserEntity : IdentityUser<int>, ICreatedEntity, IUpdatedEntity
	{
		public UserEntity()
		{
			Orders = new HashSet<OrderEntity>();
			Votes = new HashSet<ProductVoteEntity>();
		}

		public string FullName { get; set; }
		public string AvatarUrl { get; set; }
		public string Status { get; set; }

		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }

		public virtual ICollection<OrderEntity> Orders { get; set; }
		public virtual ICollection<ProductVoteEntity> Votes { get; set; }
	}
}
