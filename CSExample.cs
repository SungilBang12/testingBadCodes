using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoManager
{
    // Todo 항목 클래스
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public TodoItem(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            IsCompleted = false;
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "[완료]" : "[진행중]";
            string completedInfo = IsCompleted ? $" (완료: {CompletedAt:yyyy-MM-dd HH:mm})" : "";
            return $"{Id}. {status} {Title} - {Description}{completedInfo}";
        }
    }

    // Todo 관리 클래스
    public class TodoManager
    {
        private List<TodoItem> todos = new List<TodoItem>();
        private int nextId = 1;

        // Todo 추가
        public void AddTodo(string title, string description)
        {
            var todo = new TodoItem(nextId++, title, description);
            todos.Add(todo);
            Console.WriteLine($"✓ '{title}' 할 일이 추가되었습니다.");
        }

        // 모든 Todo 조회
        public void ListAllTodos()
        {
            if (todos.Count == 0)
            {
                Console.WriteLine("등록된 할 일이 없습니다.");
                return;
            }

            Console.WriteLine("\n=== 전체 할 일 목록 ===");
            foreach (var todo in todos)
            {
                Console.WriteLine(todo);
            }
        }

        // 완료되지 않은 Todo만 조회
        public void ListPendingTodos()
        {
            var pending = todos.Where(t => !t.IsCompleted).ToList();
            
            if (pending.Count == 0)
            {
                Console.WriteLine("진행중인 할 일이 없습니다.");
                return;
            }

            Console.WriteLine("\n=== 진행중인 할 일 ===");
            foreach (var todo in pending)
            {
                Console.WriteLine(todo);
            }
        }

        // Todo 완료 처리
        public void CompleteTodo(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            
            if (todo == null)
            {
                Console.WriteLine($"ID {id}에 해당하는 할 일을 찾을 수 없습니다.");
                return;
            }

            if (todo.IsCompleted)
            {
                Console.WriteLine($"'{todo.Title}'은(는) 이미 완료된 할 일입니다.");
                return;
            }

            todo.IsCompleted = true;
            todo.CompletedAt = DateTime.Now;
            Console.WriteLine($"✓ '{todo.Title}' 할 일을 완료했습니다!");
        }

        // Todo 삭제
        public void DeleteTodo(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            
            if (todo == null)
            {
                Console.WriteLine($"ID {id}에 해당하는 할 일을 찾을 수 없습니다.");
                return;
            }

            todos.Remove(todo);
            Console.WriteLine($"✓ '{todo.Title}' 할 일이 삭제되었습니다.");
        }

        // 통계 출력
        public void ShowStatistics()
        {
            int total = todos.Count;
            int completed = todos.Count(t => t.IsCompleted);
            int pending = total - completed;
            double completionRate = total > 0 ? (completed * 100.0 / total) : 0;

            Console.WriteLine("\n=== 통계 ===");
            Console.WriteLine($"전체 할 일: {total}개");
            Console.WriteLine($"완료: {completed}개");
            Console.WriteLine($"진행중: {pending}개");
            Console.WriteLine($"완료율: {completionRate:F1}%");
        }
    }

    // 메인 프로그램
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new TodoManager();
            bool running = true;

            Console.WriteLine("=== Todo 리스트 관리 프로그램 ===\n");

            while (running)
            {
                Console.WriteLine("\n명령어를 선택하세요:");
                Console.WriteLine("1. 할 일 추가");
                Console.WriteLine("2. 전체 목록 보기");
                Console.WriteLine("3. 진행중인 목록 보기");
                Console.WriteLine("4. 할 일 완료");
                Console.WriteLine("5. 할 일 삭제");
                Console.WriteLine("6. 통계 보기");
                Console.WriteLine("7. 종료");
                Console.Write("\n선택: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("제목: ");
                        string title = Console.ReadLine();
                        Console.Write("설명: ");
                        string description = Console.ReadLine();
                        manager.AddTodo(title, description);
                        break;

                    case "2":
                        manager.ListAllTodos();
                        break;

                    case "3":
                        manager.ListPendingTodos();
                        break;

                    case "4":
                        Console.Write("완료할 할 일 ID: ");
                        if (int.TryParse(Console.ReadLine(), out int completeId))
                        {
                            manager.CompleteTodo(completeId);
                        }
                        else
                        {
                            Console.WriteLine("올바른 ID를 입력하세요.");
                        }
                        break;

                    case "5":
                        Console.Write("삭제할 할 일 ID: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            manager.DeleteTodo(deleteId);
                        }
                        else
                        {
                            Console.WriteLine("올바른 ID를 입력하세요.");
                        }
                        break;

                    case "6":
                        manager.ShowStatistics();
                        break;

                    case "7":
                        Console.WriteLine("프로그램을 종료합니다.");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("올바른 명령어를 선택하세요.");
                        break;
                }
            }
        }
    }
}
