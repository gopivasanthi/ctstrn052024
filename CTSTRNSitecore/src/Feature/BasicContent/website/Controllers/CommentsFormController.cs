using Sitecore.Analytics.Model.Entities;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Templates;
using Sitecore.Globalization;
using Sitecore.Mvc.Presentation;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.BasicContent.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class CommentsFormController : Controller
    {
        [HttpGet]
        public ActionResult GetCommentsForm()
        {
            var commentsFormLabelItem = RenderingContext.Current.Rendering.Item;

            CommentsFormLabel commentsFormLabel = new CommentsFormLabel()
            {
                NameLabel = commentsFormLabelItem.Fields["NameLabel"].Value,
                EmailLabel = commentsFormLabelItem.Fields["EmailLabel"].Value,
                CommentLabel = commentsFormLabelItem.Fields["CommentLabel"].Value,
                SubmitButtonLabel = commentsFormLabelItem.Fields["SubmitButtonLabel"].Value,
                NamePlaceholder = commentsFormLabelItem.Fields["NamePlaceholder"].Value,
                EmailPlaceholder = commentsFormLabelItem.Fields["EmailPlaceholder"].Value,
                CommentPlaceholder = commentsFormLabelItem.Fields["CommentPlaceholder"].Value,
            };

            Comments comments = new Comments()
            {
                ID = "ID",
                Name = "Name",
                Email = "Email",
                Comment = "Comment"
            };

            CommentsFormViewModel commentsFormViewModel = new CommentsFormViewModel()
            {
                CommentsFormLabel = commentsFormLabel,
                Comments = comments
            };

            return View("/Views/TRN/BasicContent/CommentsForm.cshtml", commentsFormViewModel);
        }
        [HttpPost]
        public ActionResult GetCommentsForm(Comments input)
        {
            Comments comments = new Comments()
            {
                ID = input.ID,
                Name = input.Name,
                Email = input.Email,
                Comment = input.Comment
            };

            var masterdatabase = Sitecore.Configuration.Factory.GetDatabase("master");
            var webdatabase = Sitecore.Configuration.Factory.GetDatabase("web");
            var parentItem = Sitecore.Context.Item;
            var parentItemFromMaster = masterdatabase.GetItem(parentItem.ID);
            var templateID = new TemplateID(new ID("{AC57C652-54F7-4F97-88FC-86CBFC5E08EA}"));
            using (new SecurityDisabler())
            {
                Item itemTobeModified = null;
                var existingCommenter = parentItemFromMaster.Children.FirstOrDefault(x => x.Fields["ID"].Value == input.ID);
                if (existingCommenter != null)
                {
                    existingCommenter.Editing.BeginEdit();
                    existingCommenter.Fields["ID"].Value = input.ID;
                    existingCommenter.Fields["Name"].Value = input.Name;
                    existingCommenter.Fields["__Display name"].Value = input.Name;
                    existingCommenter.Fields["Email"].Value = input.Email;
                    existingCommenter.Fields["Comment"].Value = input.Comment;
                    existingCommenter.Editing.EndEdit();
                    itemTobeModified = existingCommenter;
                }
                else
                {

                    var newCommentItemCreated = parentItemFromMaster.Add(input.ID, templateID);
                    newCommentItemCreated.Editing.BeginEdit();
                    newCommentItemCreated.Fields["ID"].Value = input.ID;
                    newCommentItemCreated.Fields["Name"].Value = input.Name;
                    newCommentItemCreated.Fields["__Display name"].Value = input.Name;
                    newCommentItemCreated.Fields["Email"].Value = input.Email;
                    newCommentItemCreated.Fields["Comment"].Value = input.Comment;
                    newCommentItemCreated.Editing.EndEdit();
                    itemTobeModified = newCommentItemCreated;
                }
                Language language = Sitecore.Context.Language;

                PublishOptions publishOptions = new PublishOptions(masterdatabase, webdatabase, PublishMode.SingleItem, language, DateTime.Now);
                publishOptions.Deep = true;
                publishOptions.RootItem = itemTobeModified;
                Publisher publisher = new Publisher(publishOptions);
                publisher.Publish();
            }
            return View("/Views/TRN/BasicContent/CommentsConfirmation.cshtml", comments);
        }
    }
}