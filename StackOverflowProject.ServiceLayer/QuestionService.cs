using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModel;
using StackOverflowProject.ViewModel;
using StackOverflowProject.Repository;
using StackOverflowProject.ServiceLayer;
using AutoMapper;
using AutoMapper.Mappers;
using AutoMapper.Configuration;
using System.Data.Entity.Migrations.Model;

namespace StackOverflowProject.ServiceLayer
{

    public interface IQuestionService
    {
        void AddQuestion(QuestionViewModel qvm);
        void UpdateQuestionDetails(QuestionViewModel qvm);

        void UpdateQuestionVotesCount(int qid,int value);

        void UpdateQuestionAnswersCount(int qid,int value);
        void UpdateQuestionViewsCount(int qid,int value);
        void DeleteQuestion(int qid);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionID(int QuestionID,int UserID);


    }
    public class QuestionService:IQuestionService
    {
        IQuestionRepository qr;
        public QuestionService()
        {
            qr = new QuestionRepository();
        }

        public void AddQuestion(QuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<QuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = Mapper.Map<QuestionViewModel, Question>(qvm);
            qr.AddQuestion(q);

        }

        public void UpdateQuestionDetails(QuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<QuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = Mapper.Map<QuestionViewModel, Question>(qvm);
            qr.UpdateQuestionDetails(q);

        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            
            qr.UpdateQuestionVotesCount(qid,value);
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
           qr.UpdateQuestionAnswersCount(qid,value);
        }
        public void UpdateQuestionViewsCount(int qid, int value)
        {
            qr.UpdateQuestionViewsCount(qid,value);
        }

        public void DeleteQuestion(int qid)

        { 
            qr.DeleteQuestion(qid); 
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> q = qr.GetQuestions();
           
            //var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Question, QuestionViewModel>();
               cfg.CreateMap<User, UserViewModel>();
               cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            //List<CategoryViewModel> qvm=mapper.Map<List<Question>, List<QuestionViewModel>>(q);


            return qvm;
        }

        public QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID)
        {
            Question q = qr.GetQuestionsByQuestionID(QuestionID).FirstOrDefault();
            QuestionViewModel qvm = null;
            if (q != null)
            {

                //var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Question, QuestionViewModel>(q);
                foreach(var item in qvm.Answers) 
                    {
                      item.CurrentUserVoteType = 0;
                      VoteViewModel vote=item.Votes.Where(temp=>temp.UserID==UserID).FirstOrDefault();
                      if(vote!=null)
                             {
                                    item.CurrentUserVoteType=vote.VoteValue;
                              }
                    }

            }
            return qvm;
        }


    }
}
