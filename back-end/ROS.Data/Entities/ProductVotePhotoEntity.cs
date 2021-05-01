using ROS.Contracts.Entities;
using System;

namespace ROS.Data.Entities
{
	public partial class ProductVotePhotoEntity : IBaseEntity<int>, ICreatedEntity, IUpdatedEntity
	{
		public int Id { get; set; }
		public int VoteId { get; set; }
		public string Url { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }

		public virtual ProductVoteEntity Vote { get; set; }
	}
}
