// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright � 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.Worklist
{
	/// <summary>
	/// Summary description for ScheduledProcedureStepInformationEntity.
	/// </summary>
	public class ScheduledProcedureStepInformationEntity : BaseInformationEntity
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public ScheduledProcedureStepInformationEntity() : base("SCHEDULED PROCEDURE STEP") {}

		/// <summary>
		/// Set the Default Tag Type List for this Entity.
		/// </summary>
		protected override void SetDefaultTagTypeList()
		{
			TagTypeList.Add(new TagType(Tag.SCHEDULED_STATION_AE_TITLE, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_PROCEDURE_STEP_START_DATE, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_PROCEDURE_STEP_START_TIME, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.MODALITY, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_PERFORMING_PHYSICIANS_NAME, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_PROCEDURE_STEP_DESCRIPTION, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_STATION_NAME, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_PROCEDURE_STEP_LOCATION, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_ACTION_ITEM_CODE_SEQUENCE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PRE_MEDICATION, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_PROCEDURE_STEP_ID, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.REQUESTED_CONTRAST_AGENT, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.SCHEDULED_PROCEDURE_STEP_STATUS, TagTypeEnum.TagOptional));
			// plus all other attributes from the Scheduled Procedure Step Module
		}

		/// <summary>
		/// Copy from the given source Dataset into the local Dataset as defined by the
		/// default Tag Type list. In addition to the base copy we need to copy attributes from the
		/// Request Attributes Sequence (if present) and the Scheduled Procedure Step (if present).
		/// </summary>
		/// <param name="sourceDataset">Source Dataset used to populate the local Dataset.</param>
		public override void CopyFrom(AttributeSet sourceDataset)
		{
			// perform base copy
			base.CopyFrom(sourceDataset);

			// check if the Request Attributes Sequence is available in the source dataset
			DvtkData.Dimse.Attribute requestAttributesSequence = sourceDataset.GetAttribute(Tag.REQUEST_ATTRIBUTES_SEQUENCE);
			if (requestAttributesSequence != null)
			{
				SequenceOfItems sequenceOfItems = (SequenceOfItems)requestAttributesSequence.DicomValue;
				if (sequenceOfItems.Sequence.Count == 1)
				{
					DvtkData.Dimse.SequenceItem item = sequenceOfItems.Sequence[0];

					// copy item attributes too
					base.CopyFrom(item);
				}
			}

			// check if the Scheduled Procedure Step Sequence is available in the source dataset
			DvtkData.Dimse.Attribute scheduledProcedureStepSequence = sourceDataset.GetAttribute(Tag.SCHEDULED_PROCEDURE_STEP_SEQUENCE);
			if (scheduledProcedureStepSequence != null)
			{
				SequenceOfItems sequenceOfItems = (SequenceOfItems)scheduledProcedureStepSequence.DicomValue;
				if (sequenceOfItems.Sequence.Count == 1)
				{
					DvtkData.Dimse.SequenceItem item = sequenceOfItems.Sequence[0];

					// copy item attributes too
					base.CopyFrom(item);
				}
			}
		}

		/// <summary>
		/// Check if the given match dataset is found in the local dataset using the default Tag Type list. 
		/// A check is made to see if all the attributes in the given match dataset are present in the local
		/// dataset. In addition to the base match we need to try to match attributes from the
		/// Request Attributes Sequence (if present) or Scheduled Procedure Step Sequence (if present).
		/// </summary>
		/// <param name="matchDataset">Match dataset to check.</param>
		/// <returns>Boolean indicating if the match attributes are present in the local dataset.</returns>
		public override bool IsFoundIn(AttributeSet matchDataset)
		{
			bool isFoundIn = base.IsFoundIn(matchDataset);

			if (isFoundIn == false)
			{
				// check if the Request Attributes Sequence is available in the match dataset
				DvtkData.Dimse.Attribute requestAttributesSequence = matchDataset.GetAttribute(Tag.REQUEST_ATTRIBUTES_SEQUENCE);
				if (requestAttributesSequence != null)
				{
					SequenceOfItems sequenceOfItems = (SequenceOfItems)requestAttributesSequence.DicomValue;
					if (sequenceOfItems.Sequence.Count == 1)
					{	
						// set up a temporary tag list to check the relevant tags in the Request Attributes Sequence
						TagTypeList itemTagTypeList = new TagTypeList();
						itemTagTypeList.Add(new TagType(Tag.SCHEDULED_PROCEDURE_STEP_DESCRIPTION, TagTypeEnum.TagOptional));
						itemTagTypeList.Add(new TagType(Tag.SCHEDULED_ACTION_ITEM_CODE_SEQUENCE, TagTypeEnum.TagOptional));
						itemTagTypeList.Add(new TagType(Tag.SCHEDULED_PROCEDURE_STEP_ID, TagTypeEnum.TagOptional));
					
						DvtkData.Dimse.SequenceItem item = sequenceOfItems.Sequence[0];

						// check if found in item
						isFoundIn = base.IsFoundIn(itemTagTypeList, item);
					}
				}

				// check if the Scheduled Procedure Step Sequence is available in the match dataset
				DvtkData.Dimse.Attribute scheduledProcedureStepSequence = matchDataset.GetAttribute(Tag.SCHEDULED_PROCEDURE_STEP_SEQUENCE);
				if (scheduledProcedureStepSequence != null)
				{
					SequenceOfItems sequenceOfItems = (SequenceOfItems)scheduledProcedureStepSequence.DicomValue;
					if (sequenceOfItems.Sequence.Count == 1)
					{	
						DvtkData.Dimse.SequenceItem item = sequenceOfItems.Sequence[0];

						// check if found in item - use default tag list
						isFoundIn = base.IsFoundIn(item);
					}
				}
			}

			return isFoundIn;
		}
	}
}
