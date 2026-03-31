using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModel;

namespace StackOverflowProject.Repository
{
    public interface IVotesRepository
    {
        void UpdateVote(int aid,int uid,int value);
    }
    public class VotesRepository:IVotesRepository
    {
        StackOverflowDatabaseDbcontext db;
        public VotesRepository()
            { 
               db=new StackOverflowDatabaseDbcontext();
            }

        public void UpdateVote(int aid,int uid,int value) 
        {
            int UpdateValue;
            if(value>0) UpdateValue=1;
            else if(value<0) UpdateValue=-1;
            else UpdateValue=0;
            Vote vote=db.Votes.Where(temp=>temp.AnswerID==aid && temp.UserID==uid).FirstOrDefault();
            if(vote!=null) 
                {
                   vote.VoteValue=UpdateValue;
                }
            else
            {
                Vote newvote=new Vote() { AnswerID=aid,UserID=uid,VoteValue=UpdateValue };
                db.Votes.Add(newvote);
            }
            db.SaveChanges();
        }
    }
}
