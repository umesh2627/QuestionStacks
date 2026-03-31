using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModel;

namespace StackOverflowProject.Repository
{

    public interface IQuestionRepository
    {
        void AddQuestion(Question q);

        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int qid,int value);

        void UpdateQuestionAnswersCount(int qid, int value);

        void UpdateQuestionViewsCount(int qid,int value);
        void DeleteQuestion(int qid);

        List<Question> GetQuestions();

        List<Question> GetQuestionsByQuestionID(int qid);
    }
    public class QuestionRepository:IQuestionRepository
    {
        StackOverflowDatabaseDbcontext db;

        public QuestionRepository()
        {
            db= new StackOverflowDatabaseDbcontext();
        }

        public void AddQuestion(Question q)
        {
            db.Questions.Add(q);
            db.SaveChanges();
        }

        public void UpdateQuestionDetails(Question q)
        {
           Question Qt=db.Questions.Where(temp=>temp.QuestionID==q.QuestionID).FirstOrDefault();
            if (Qt!=null)
            {
              Qt.QuestionName=q.QuestionName;
              Qt.QuestionDateAndTime=q.QuestionDateAndTime;
              Qt.CategoryID=q.CategoryID;
              db.SaveChanges() ;
            }
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            Question Qt = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (Qt != null)
            {
               Qt.VotesCount=value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            Question Qt = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (Qt != null)
            {
                Qt.AnswersCount = value;
                db.SaveChanges();
            }
        }
        public void UpdateQuestionViewsCount(int qid,int value)
        {
            Question Qt = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (Qt != null)
            {
                Qt.ViewsCount = value;
                db.SaveChanges();
            }
        }
         public void DeleteQuestion(int qid) 
         {
              Question Qt=db.Questions.Where(temp=>temp.QuestionID== qid).FirstOrDefault();
              if (Qt != null) 
                {
                  db.Questions.Remove(Qt);
                  db.SaveChanges();
                }
         }

        public List<Question> GetQuestions()
        {
            List<Question> Qt = db.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();
            return Qt;
        }

        public List<Question> GetQuestionsByQuestionID(int qid)
        {
            List<Question> Qt = db.Questions.Where(temp => temp.QuestionID == qid).ToList();
            return Qt;
        }


    }

}
