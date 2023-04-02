using System;
using System.Collections.Generic;

namespace ConsoleApp45
{
    // Memento класс, который сохраняет состояние текстового редактора
    public class TextEditorMemento
    {
        public string Text { get; private set; }

        public TextEditorMemento(string text)
        {
            Text = text;
        }
    }

    // Originator класс, который создает Memento и восстанавливает состояние из него
    public class TextEditor
    {
        private string Text;
        private readonly Stack<TextEditorMemento> mementos = new Stack<TextEditorMemento>();
        private int undoCount = 0;

        public TextEditor()
        {
            Text = "";
        }

        public void AppendText(string text)
        {
            mementos.Push(new TextEditorMemento(Text));
            Text += text;
            undoCount = 0;
            if (mementos.Count > 256)
            {
                mementos.Dequeue();
            }
        }

        public void Undo()
        {
            if (mementos.Count > 0)
            {
                Text = mementos.Pop().Text;
                undoCount++;
            }
        }

        public void Redo()
        {
            if (undoCount > 0)
            {
                mementos.Push(new TextEditorMemento(Text));
                undoCount--;
            }
        }

        public string GetText()
        {
            return Text;
        }
    }

    public class Program
    {
        public static void Main()
        {
            var textEditor = new TextEditor();
            textEditor.AppendText("Hello, ");
            Console.WriteLine(textEditor.GetText()); // выводит "Hello, "
            textEditor.AppendText("world!");
            Console.WriteLine(textEditor.GetText()); // выводит "Hello, world!"
            textEditor.Undo();
            Console.WriteLine(textEditor.GetText()); // выводит "Hello, "
            textEditor.Redo();
            Console.WriteLine(textEditor.GetText()); // выводит "Hello, world!"
        }
    }
}
