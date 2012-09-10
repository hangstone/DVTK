//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright � 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_ae_title.h"
#include "Iglobal.h"		// Global component interface
#include "valrules.h"


//>>===========================================================================		

ACSE_AE_TITLE_CLASS::ACSE_AE_TITLE_CLASS()

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// constructor activities
	quotedValueM = true;
} 

//>>===========================================================================		

ACSE_AE_TITLE_CLASS::~ACSE_AE_TITLE_CLASS()

//  DESCRIPTION     : Destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// destructor activities
} 

//>>===========================================================================		

bool ACSE_AE_TITLE_CLASS::checkSyntax()

//  DESCRIPTION     : Check parameter syntax.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// check parameter syntax
    int length = valueM.length();     	            
	bool result = true;
	
    if (length > AE_LENGTH)
	{
		addMessage(VAL_RULE_D_AE_1, "Value length %d exceeds maximum length %d - truncated",
			length, AE_LENGTH);
		length = AE_LENGTH;
		result = false;
	}
	
	if (length)
	{
		//
		// check for ESC, LF, FF or CR				
		//
		for (int i = 0; i < length; i++) 
		{
			if (valueM[i] == NullChar)
			{
				break;
			}
			
			if ((valueM[i] == LFChar) ||
				(valueM[i] == FFChar) ||
				(valueM[i] == CRChar) ||
				(valueM[i] == ESCChar)) 
			{
				addMessage(VAL_RULE_D_AE_2, "Unexpected Control Character 0x%02X at offset %d",
					(int) valueM[i], i);
				result = false;
			}
		}
		
		// only check if we still don't have any errors
		if (result == true)
		{
			// check for 16 SPACES
			int i;
			for (i = 0; i < length; i++) 
			{
				if (valueM[i] != SpaceChar) 
				{
					break;
				}
			}
			
			if (i == AE_LENGTH) 			
			{
				addMessage(VAL_RULE_D_AE_3, "Value contains only SPACE Characters");
				result = false;
			}
		}
	}
	
	return result;
} 

//>>===========================================================================		

bool ACSE_AE_TITLE_CLASS::checkRange()

//  DESCRIPTION     : Check parameter range.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// check parameter range
	return true;
} 

//>>===========================================================================		

bool ACSE_AE_TITLE_CLASS::checkReference(string refValue)

//  DESCRIPTION     : Check parameter against reference value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	bool result = true;
	
	// check parameter against reference value
	if (refValue.length())
	{
		result = checkStringDifferences((char*) valueM.c_str(), (char*) refValue.c_str(), AE_LENGTH, true, false);
	}
	
	// return result
	return result;
} 

