# CustomImplementations

A growing collection of custom implementations of commonly used services, middleware, and utilities in C#. This repository is intended to demonstrate how core features can be built from scratch — useful for learning, rapid prototyping, or lightweight applications.

---

## ✅ Current Implementation

### 🔒 RateLimitService Middleware

The **RateLimitService** is a thread-safe, configurable middleware that limits the number of requests from each client (identified by IP). It’s designed to prevent abuse, brute force attempts, and reduce server load.

#### ✨ Features

- 🔁 Per-IP request tracking with `ConcurrentDictionary`
- ⚙️ Configurable limit and time window via `appsettings.json`
- ⛔ Spammer detection with temporary blocking
- 🧵 Thread-safe and lightweight
- 🧾 Built-in logging with a simple `LoggerService`

---

## 📦 Configuration

Configure your limits in `appsettings.json`:

```json
"RateLimitOptions": {
  "Limit": 100,
  "TimeWindow": "00:01:00",         // 1 minute
  "SpammersLimit": 200,
  "SpammerBlockTime": "00:10:00"    // 10 minutes
}
```

## 🚀 Roadmap

### ✅ Implemented

- [x] **RateLimiter Middleware** – Per-IP request limiting with spammer detection.

### 🧠 Coming Soon

- [ ] **Custom Caching Layer** – Lightweight in-memory caching with expiration support.
- [ ] **Retry Policy Middleware** – Simple retry mechanism for transient failures.
- [ ] **In-Memory Pub/Sub System** – Basic publisher-subscriber architecture using events.
- [ ] **Job Scheduling Service** – Schedule and run background tasks on custom intervals.

---

## 🤝 Contributing

Want to contribute a custom implementation?

1. Fork the repository
2. Create a new folder for your feature
3. Add your implementation
4. Submit a pull request

We welcome all contributions — whether it's new features, improvements, or bug fixes.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 💬 Author

Made with ❤️ by [@aakashOye](https://github.com/aakashOye)
