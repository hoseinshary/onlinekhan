﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Assay;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ServiceLayer.Services
{
    public class AssayService
    {
        private const string Title = "آزمون";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Assay> _assays;

        private readonly Lazy<QuestionService> _questionService;

        public AssayService(IUnitOfWork uow, Lazy<QuestionService> questionService)
        {
            _uow = uow;
            _assays = uow.Set<Assay>();
            _questionService = questionService;
        }

        /// <summary>
        /// گرفتن  آزمون با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssayViewModel GetById(int id)
        {
            return _assays
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AssayViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن سوالات یک آزمون
        /// </summary>
        /// <returns></returns>
        public IList<QuestionAssayViewModel> GetAllQuestion(AssayCreateViewModel assayGetQuestionsViewModel)
        {
            List<QuestionAssayViewModel> questionsReturn = new List<QuestionAssayViewModel>();
            if (assayGetQuestionsViewModel.RandomQuestion)
            {
                foreach (var lesson in assayGetQuestionsViewModel.Lessons)
                {

                    QuestionAssayViewModel lessonItem = new QuestionAssayViewModel();
                    lessonItem.Questions= new List<QuestionViewModel>();
                    lessonItem.LessonId = lesson.Id;



                    var q = _questionService.Value.GetAllByTopicIdsForAssay(
                        lesson.Topics.Select(x => x.Id).ToList(), assayGetQuestionsViewModel.Page,
                        20);
                    

                    lessonItem.Questions.AddRange(q);
                

                    questionsReturn.Add(lessonItem);
                }

                return questionsReturn;
            }
            else
            {
                foreach (var lesson in assayGetQuestionsViewModel.Lessons)
                {
                    QuestionAssayViewModel lessonItem = new QuestionAssayViewModel();
                    lessonItem.LessonId = lesson.Id;
                    //foreach (var topic in lesson.Topics)
                    //{
                    var q = _questionService.Value.GetAllByTopicIdsForAssay(
                        lesson.TopicIds.ToList(), assayGetQuestionsViewModel.Page,
                        20);
                    if (q.Count >0)
                            lessonItem.Questions.AddRange(q);
                      
                    //}
                     lessonItem.Questions.Distinct();
                        
                    questionsReturn.Add(lessonItem);
                }

                return questionsReturn;
            }

        }

        /// <summary>
        /// گرفتن همه آزمون ها
        /// </summary>
        /// <returns></returns>
        public IList<AssayViewModel> GetAll()
        {
            return _assays
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AssayViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت آزمون
        /// </summary>
        /// <param name="assayViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(AssayCreateViewModel assayViewModel)
        {
            var assay = Mapper.Map<Assay>(assayViewModel);



            if (assayViewModel.NumberOfVarient == AssayVarient.A)
            {
                assay.File1 = Guid.NewGuid().ToString();
            }
            else
            {

            }

            assay.Lessons = new List<Lesson>();

            foreach (var lesson in assayViewModel.Lessons)
            {
                Lesson tempLesson = new Lesson()
                {
                    Id = lesson.Id
                };
                _uow.MarkAsUnChanged(tempLesson);
                assay.Lessons.Add(tempLesson);

                foreach (var question in lesson.Questions)
                {
                    if (assayViewModel.NumberOfVarient == AssayVarient.A)
                    {
                        assay.QuestionsFile1 += assay.QuestionsFile1 =="" || assay.QuestionsFile1 is null ? question.FileName  : ";" + question.FileName;
                        assay.QuestionsAnswer1 += assay.QuestionsAnswer1 == ""  || assay.QuestionsAnswer1 is null ?  question.AnswerNumber.ToString() :  ";" + question.AnswerNumber;
                        assay.AssayQuestions.Add(new AssayQuestion()
                        {
                            QuestionId = question.Id,
                            LessonId = lesson.Id
                        });
                    }
                    else
                    {
                        
                    }
                    
                }

            }
            _assays.Add(assay);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            if (serverResult.MessageType == MessageType.Success)
            {
                string dir = SitePath.ToAbsolutePath( SitePath.AssayRelPath + assay.File1);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // Open a doc file.
                var app = new Microsoft.Office.Interop.Word.Application();
                app.Visible = true;
                var target = app.Documents.Add(Visible: true);

                int questionNumber = 1;
                bool firstTimeFlag = false;
                foreach (var lesson in assayViewModel.Lessons)
                {
                   
                  
                    foreach (var question in lesson.Questions)
                    {
                        var wordFilename = question.QuestionWordPath;
                        var source = app.Documents.Open(wordFilename, Visible: true);
                        if (!firstTimeFlag)
                        {
                            //تریک درست شدن گزینه ها 
                            source.ActiveWindow.Selection.WholeStory();
                            source.ActiveWindow.Selection.Copy();
                            target.ActiveWindow.Selection.Paste();
                            target.ActiveWindow.Selection.WholeStory();
                            target.ActiveWindow.Selection.Delete();
                            firstTimeFlag = true;
                            addTextToDoc(app, lesson.Name);
                            
                            // اضافه کردن عدد اول سوالات 
                            source.Paragraphs[2].Range.Characters[1].InsertBefore(questionNumber + "- ");

                        }
                        else
                        {
                            // اضافه کردن عدد اول سوالات 

                            source.Paragraphs[1].Range.Characters[1].InsertBefore(questionNumber + "- ");
                            
                           // source.Paragraphs[1].Range.Text = /*questionNumber + "-" +*/ source.Paragraphs[1].Range.Text;
                        }

                        for (int i = 1; i <= source.Paragraphs.Count; i++)
                        {
                            if(source.Paragraphs[i].Range.Text == "\r")
                                source.Paragraphs[i].Range.Delete();

                        }

                      

                        source.ActiveWindow.Selection.WholeStory();
                        source.ActiveWindow.Selection.Copy();
                        target.ActiveWindow.Selection.Paste();

                        source.Close(WdSaveOptions.wdDoNotSaveChanges);
                        questionNumber++;
                    }
                }
                target.SaveAs(dir +"/" +assay.File1 + ".docx", WdSaveFormat.wdFormatXMLDocument);

                target.SaveAs(dir + "/" + assay.File1 + ".pdf", WdSaveFormat.wdFormatPDF);
                target.Close();
                app.Quit();


            }
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(assay.Id);

            return clientResult;
        }

        public void addTextToDoc(Microsoft.Office.Interop.Word.Application app ,  string text)
        {
            Microsoft.Office.Interop.Word.Selection currentSelection = app.Selection;

            // Store the user's current Overtype selection
            bool userOvertype = app.Options.Overtype;

            // Make sure Overtype is turned off.
            if (app.Options.Overtype)
            {
                app.Options.Overtype = false;
            }

            // Test to see if selection is an insertion point.
            if (currentSelection.Type == Microsoft.Office.Interop.Word.WdSelectionType.wdSelectionIP)
            {
                currentSelection.TypeText(text);
                currentSelection.TypeParagraph();
            }
            else
            if (currentSelection.Type == Microsoft.Office.Interop.Word.WdSelectionType.wdSelectionNormal)
            {
                // Move to start of selection.
                if (app.Options.ReplaceSelection)
                {
                    object direction = Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseStart;
                    currentSelection.Collapse(ref direction);
                }
                currentSelection.TypeText(text);
                currentSelection.TypeParagraph();
            }
            else
            {
                // Do nothing.
            }

            // Restore the user's Overtype selection
            app.Options.Overtype = userOvertype;
        }

        /// <summary>
        /// ویرایش آزمون
        /// </summary>
        /// <param name="assayViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(AssayCreateViewModel assayViewModel)
        {
            var assay = Mapper.Map<Assay>(assayViewModel);
            _uow.MarkAsChanged(assay);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(assay.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف آزمون
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var assayViewModel = GetById(id);
            if (assayViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var assay = Mapper.Map<Assay>(assayViewModel);
            _uow.MarkAsDeleted(assay);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }



        
    }
}
