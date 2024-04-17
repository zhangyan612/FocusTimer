import tkinter as tk
from tkinter import messagebox
import time
import winsound
import datetime

def start_timer():
    task = task_text.get("1.0", "end-1c")
    duration = int(duration_var.get())
    log_file = datetime.datetime.now().strftime("%Y-%m-%d") + ".txt"

    with open(log_file, "a") as file:
        file.write(f"Task: {task}, Started at: {datetime.datetime.now()}, Duration: {duration} minutes\n")

    start_button.pack_forget()
    pause_button.pack()
    stop_button.pack()

    countdown(duration * 60)

    while True:
        user_input = messagebox.askquestion("Task Done", "Is your task done?")
        if user_input == "yes":
            with open(log_file, "a") as file:
                file.write(f"Task: {task}, Completed at: {datetime.datetime.now()}\n")
            break
        else:
            countdown(20 * 60)

def pause_timer():
    global paused
    paused = not paused

def stop_timer():
    global stop
    stop = True

def countdown(t):
    global paused, stop
    paused = False
    stop = False

    while t:
        if stop:
            break

        if not paused:
            mins, secs = divmod(t, 60)
            timeformat = '{:02d}:{:02d}'.format(mins, secs)
            time_label.config(text=timeformat)
            root.update()
            time.sleep(1)
            t -= 1

    if not stop:
        winsound.Beep(2500, 1000)

root = tk.Tk()
root.geometry('800x600')  # Set the default window size
root.configure(bg='black')  # Set the background color to dark

task_label = tk.Label(root, text="Enter Task:", bg='black', fg='white')
task_label.pack()

task_text = tk.Text(root, height=5, bg='grey', fg='white')
task_text.pack()

duration_var = tk.StringVar(root)
duration_var.set("20")  # default value

duration_option = tk.OptionMenu(root, duration_var, "20", "30")
duration_option.config(bg='grey', fg='white')  # Set the color of the option menu
duration_option.pack()

start_button = tk.Button(root, text="Start", command=start_timer, bg='grey', fg='white')
start_button.pack()

pause_button = tk.Button(root, text="Pause/Resume", command=pause_timer, bg='grey', fg='white')
stop_button = tk.Button(root, text="Stop", command=stop_timer, bg='grey', fg='white')

time_label = tk.Label(root, text="", font=("Helvetica", 24), bg='black', fg='white')
time_label.pack()

root.mainloop()
