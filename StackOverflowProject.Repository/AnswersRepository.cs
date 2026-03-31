using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModel;

namespace StackOverflowProject.Repository
{

    public interface IAnswersRepository
    {
        void AddAnswer (Answer a);

        void UpdateAnswer(Answer a);

        void UpdateAnswerVotesCount (int aid,int uid,int value);
        void DeleteAnswer (int aid);

       List<Answer> GetAnswersByQuestionID(int qid);

        List<Answer> GetAnswersByAnswerID(int aid);

    }
    public class AnswersRepository:IAnswersRepository
    {
        StackOverflowDatabaseDbcontext db;
        IQuestionRepository qr;
        IVotesRepository vr;
        public AnswersRepository()
        {
            db = new StackOverflowDatabaseDbcontext();
            qr=new QuestionRepository();
            vr=new VotesRepository();
        }

        public void AddAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(a.QuestionID,1);
        }

        public void UpdateAnswer(Answer a)
        {
            Answer aw=db.Answers.Where(temp=>temp.AnswerID==a.AnswerID).FirstOrDefault();
            if(aw!=null)
            {
               aw.AnswerText=a.AnswerText;
                db.SaveChanges();
                
            }    
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer aw = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if (aw != null)
            {
                aw.VotesCount=value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(aw.QuestionID, value);
                 vr.UpdateVote(aid,uid,value);
            }
        }

        public void DeleteAnswer(int aid)
        {
            Answer ans=db.Answers.Where(temp=>temp.AnswerID==aid).FirstOrDefault();
            if (ans!=null)
            {
                db.Answers.Remove(ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.QuestionID,-1);
            }
        }

        public List<Answer> GetAnswersByQuestionID(int qid)
        {
            List<Answer> ans=db.Answers.Where(temp=>temp.QuestionID==qid).OrderByDescending(temp=>temp.AnswerDateAndTime).ToList(); 
            return ans;
        }

        public List<Answer> GetAnswersByAnswerID(int aid)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.AnswerID == aid).ToList();
            return ans;
        }


    }

}
