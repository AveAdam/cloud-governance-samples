﻿namespace Cloud.Governance.Samples.Sdk
{
    #region using directives
    using AvePoint.GA.WebAPI;
    using AvePoint.GA.WebAPI.Models;
    using System;
    using System.Collections.Generic;
    #endregion

    /// <summary>
    /// The sample for O365 Group/Team provision
    /// </summary>
    public class CreateTeamSample
    {
        private ICommonService commonService;
        private IRequestService requestService;

        /// <summary>
        /// Submit a request to create O365 Group/Team
        /// </summary>
        /// <returns>The result of submitting request</returns>
        public Boolean SubmitRequest()
        {
            this.Initialize();
            var template = this.GetRequestTemplate();
            var requestInfo = this.SetValue(template);
            return this.SaveAndSubmit(requestInfo);
        }

        /// <summary>
        /// Initialize the API client
        /// </summary>
        private void Initialize()
        {
            //Your Cloud Governance user name
            var username = "";
            //Your Cloud Governance password
            var password = "";
            GaoApi.Init(Region.EastUS, username, password);
            this.commonService = GaoApi.Create<ICommonService>();
            this.requestService = GaoApi.Create<IRequestService>();
        }

        /// <summary>
        /// Get request template to specified Create O365 Group/Team service
        /// </summary>
        /// <returns>Request template</returns>
        private APIRequestCreateGroup GetRequestTemplate()
        {
            //The ID of Create O365 Group/Team service
            var serviceId = new Guid("");
            var serviceInfo = this.commonService.Get(serviceId);
            return serviceInfo.APIRequest as APIRequestCreateGroup;
        }

        /// <summary>
        /// Set the request information
        /// </summary>
        /// <param name="template">Request template</param>
        /// <returns>Request information</returns>
        private APIRequestCreateGroup SetValue(APIRequestCreateGroup template)
        {
            var requestInfo = template;

            #region Required

            //Request Summary
            requestInfo.RequestSummary = "Create Team Sample";
            //Name
            requestInfo.GroupName = "Sample";
            //ID
            requestInfo.GroupEmail = "";
            //Owners
            requestInfo.Owners = new List<String> { "" };
            //Members
            requestInfo.Members = new List<String> { "" };
            //Primary Contact
            requestInfo.PrimaryContactUser = "";
            //Secondary Contact
            requestInfo.SecondaryContactUser = "";
            //Enable Team
            requestInfo.EnableTeamCollaboration = true;

            #endregion

            #region Not Required

            //Request Description
            requestInfo.Description = "Sample";
            //Group Description
            requestInfo.GroupDescription = "Sample";

            this.SetMetadataValue(requestInfo);

            #endregion

            return requestInfo;
        }

        /// <summary>
        /// Set request metadata value
        /// </summary>
        /// <param name="requestInfo">Request information</param>
        private void SetMetadataValue(APIRequest requestInfo)
        {
            //Metadata Name
            var metadataName = "";
            var metadata = requestInfo.MetadataList.Find(m => m.Name.Equals(metadataName));
            if (metadata != null)
            {
                //Metadata Value
                metadata.Value = "";
            }
        }

        /// <summary>
        /// Save and submit Create O365 Group/Team request
        /// </summary>
        /// <param name="requestInfo">Request information</param>
        /// <returns>The result of submitting request</returns>
        private Boolean SaveAndSubmit(APIRequestCreateGroup requestInfo)
        {
            var requestId = this.requestService.Save(requestInfo);
            return this.requestService.Submit(requestId);
        }
    }
}
