using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GA_Queue_DoublyLinkedList_DanielMinty
{
    internal class Queue<T>
    {
        //Front is the oldest node in the queue, nodes will be removed from the front of the queue
        private QueueNode<T> front;
        //Back is the youngest node in the queue, nodes will be added to the back of the queue
        private QueueNode<T> back;

        //Number of nodes in the Queue
        private int count;
        public int Count { get { return count; } }

        //Constructor
        public Queue()
        {
            front = null;
            back = null;
            count = 0;
        }

        //Enqueue attaches a node with the given value to the back of the queue
        public void Enqueue(T value)
        {
            //If the queue is empty, the new node will be the front and back of the queue
            if (count == 0)
            {
                front = new QueueNode<T>(value);
                back = front;
                count++;
                return;
            }

            //Attach the new node to the end of the queue
            back.Behind = new QueueNode<T>(back, value);
            //Make the new node the new back of the queue
            back = back.Behind;
            count++;
        }

        //Dequeue removes the front node from the queue and returns it's value
        public T Dequeue()
        {
            //If the queue is empty, throw an exeption
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            //Store the value to be returned
            T result = front.Value;

            //If there is only one node in the queue, empty the queue and return the value
            if (count == 1)
            {
                front = null;
                back = null;
                count = 0;
                return result;
            }
            //If more than one node in the queue
            //Assign the new front node
            front = front.Behind;
            //remove the old front node
            front.InFront = null;
            //update count
            count--;
            //return the value
            return result;
        }

        //Peek returns the value of the front node without removing it from the queue
        public T Peek()
        {
            //If the queue is empty, throw an exeption
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }
            //The queue is not empty
            //return the value of the front node
            return front.Value;
        }

        //Clear removes each node from the queue, one at a time from front to back
        public void Clear()
        {
            //for each element that's not the last element
            for (int i = 1; i < this.count; i++)
            {
                //Remove the front element
                front = front.Behind;
                front.InFront.Behind = null;
                front.InFront = null;
            }
            //only one element remains, remove it
            front = null;
            back = null;
            count = 0;
        }

        //Node class
        private class QueueNode<T>
        {
            //The node next to this one toward the front (old) end of the queue
            private QueueNode<T> inFront;
            //The value stored in this node
            private T value;
            //The node next to this one toward the back (young) end of the queue
            private QueueNode<T> behind;

            //Properties
            public QueueNode<T> InFront { get { return inFront; } set { inFront = value; } }
            public T Value { get { return value; } set { this.value = value; } }
            public QueueNode<T> Behind { get { return behind; } set { behind = value; } }

            //Constructors
            //Only assigns value
            public QueueNode(T value)
            {
                inFront = null;
                this.value = value;
                behind = null;
            }

            //Assigns value and inFront
            public QueueNode(QueueNode<T> inFront, T value)
            {
                this.inFront = inFront;
                this.value = value;
                behind = null;
            }

        }//class QueueNode

    }//class Queue

}//namespace
