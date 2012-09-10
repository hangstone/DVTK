/*
 * Terrier - Terabyte Retriever
 * Webpage: http://ir.dcs.gla.ac.uk/terrier
 * Contact: terrier{a.}dcs.gla.ac.uk
 * University of Glasgow - Department of Computing Science
 * Information Retrieval Group
 *
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
 * the License for the specific language governing rights and limitations
 * under the License.
 *
 * The Original Code is Request.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.querying;
import java.util.Hashtable;
import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.querying.parser.Query;
/**
* Request
* SearchRequest contains the details of the search engine for one query, including the query itself,
* the resultset, and the controls. Controls set application specific parameters, which often load
* post proceses, and postfilters, or set matching parameter. These are fundamentally, the <b>names</b>
of the modules to be used for each stage of a query:
* <ul>
* <li>The matching and weighting modules to be used</li>
* <li>postProcess modules to apply transforms to the resultset</li>
* <li>postFilter modules to apply transforms to the resultset</li>
* </ul>
* A Request is an implementation of the SearchRequest interface.
* Additionally, the Request holds all the controls set for this query.
* @version $Revision: 1.1 $
* @author Craig Macdonald, June &amp; October 2004
*/
class Request implements SearchRequest
{
	/** does the query have any terms. Used by Manager.runMatching() to short circuit the
	  * matching process - if this is set, then a resultset with 0 documents is created
	  * automatically.*/
	protected boolean EmptyQuery = false;
	/** QueryID - used by TREC querying and output format for supporting relevance assessments */
	protected String QueryID = "";
	/** Name of module to be used at weighting module to be used at matching &amp; weighting stage. */
	protected String WeightingModule;
	/** Name of module to be used at matching module to be used at matching &amp; weighting stage. */
	protected String MatchingModule;
	/** Controls are querying stage flags and variables. These are typically set by defaults
	 * in the configuration file, or by flags in the query itself (if querying.HTTPSearchEngine is used).
	 * They often depict which pre, matching, post and output modules will be used. */
	protected Hashtable Control = new Hashtable();
	/** This contains the parsed syntax tree of the query
	 */
	protected Query q;
	/** This is <b>the</b> resultset of the query. It is provided by the matching/weighting stage,
	 * and may be operated on (eg decorated) by PostProcesses and PostFilters
	 */
	protected ResultSet resultSet;
	/** This is an aggregated form of the query terms, suitable for matching, which requires
	  * term frequencies for each term. */
	protected MatchingQueryTerms matchingTerms;
	/** Set the matching model and weighting model that the Manager should use for this query
	  * @param MatchingModelName the String class name that should be used
	  * @param WeightingModelName the String class name that should be used */
	public void addMatchingModel(String MatchingModelName, String WeightingModelName){
		WeightingModule = WeightingModelName;
		MatchingModule = MatchingModelName;
	}
 	/** Set the query to be a parsed Query syntax tree, as generated by the Terrier query parser
	  * @param q The Query object syntax tree */
	public void setQuery(Query q)
	{
		this.q = q;
	}
	/** Set a unique identifier for this query request.
	  * @param qid the unique string identifier*/
	public void setQueryID(String qid)
	{
		QueryID = qid;
	}
	/** Set a control named to have value Value. This is essentially a
	  * hashtable wrappers. Controls are used to set properties of the
	  * retrieval process.
	  * @param Name the name of the control to set the value of.
	  * @param Value the value that the control should take. */
	public void setControl(String Name, String Value)
	{
		Control.put(Name, Value);
	}
	/** Returns the value of the control. Null or empty string if not set.
	  * @return the value. */
	public String getControl(String Name)
	{
		Object o = Control.get(Name);
		if (o == null)
			return "";
		return (String)o;
	}
	/** Returns the resultset generated by the query. Before retrieving this
	  * you probably need to have run Manager.runMatching, and (optionally, at
	  * your own risk) Manager.runPostProcesses() and Manager.runPostFiltering().
	  * @returns the resultset - ie the set of document Ids and their scores. */
	public ResultSet getResultSet()
	{
		return resultSet;
	}
	/** Returns the query id as set by setQueryID(String).
	  * @return the query Id as a string. */
	public String getQueryID()
	{
		return QueryID;
	}
	/** Get the Query syntax tree
	  * @return The query object as set with setQuery.
	  * */
	public Query getQuery()
	{
		return q;
	}
	/** Set if the query input had no terms.
	  * @return true if the query has no terms. */
	public boolean isEmpty()
	{
		return EmptyQuery;
	}
	/** returns the name of the weighting model that should be used for retrieval */
	public String getWeightingModel()
	{
		return WeightingModule;
	}
	/** returns the name of the matching model that should be used for retrieval */
	public String getMatchingModel()
	{
		return MatchingModule;
	}
	/** Set the result set returned by this object to be this results.
	  * @return results the resultset to be returned as the answer to this query */
	public void setResultSet(ResultSet results)
	{
		resultSet = results;
	}
	/** Get the entire hashtable used for storing controls for this query */
	public Hashtable getControlHashtable()
	{
		return Control;
	}
	/** force this query to be seen as containing (no) terms.
	  * @param in set to true for query to be seen empty. */
	public void setEmpty(boolean in)
	{
		EmptyQuery = in;
	}
	/** Used by runPreProcessing after the query tree has been
	  * aggregated into a list of terms, each containing frequencies.
	  * @param mqts The matchingqueryterms to use for matching.
	  */
	public void setMatchingQueryTerms(MatchingQueryTerms mqts)
	{
		matchingTerms = mqts;
	}
	/** Return the MatchingQueryTerms object to use for matching */
	public MatchingQueryTerms getMatchingQueryTerms()
	{
		return matchingTerms;
	}
	/** Use this hashtable to store controls and their values in */
	public void setControls(Hashtable controls)
	{
		Control = controls;
	}
}
