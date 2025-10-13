using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    protected GameObject value;
    protected Node next;

    public Node(GameObject value, Node next){
        this.value = value;
        this.next = next;
    }
}
