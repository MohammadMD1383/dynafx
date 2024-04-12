#include <QApplication>
#include <QQmlApplicationEngine>
#include <QQmlContext>
#include "SoundLooper.hpp"

using namespace Qt::StringLiterals;

// QObject::connect(&engine, &QQmlApplicationEngine::objectCreated, &app,
//                  [url](const QObject* obj, const QUrl&objUrl) {
// 	                 if (!obj && url == objUrl)
// 		                 QCoreApplication::exit(-1);
//                  },
//                  Qt::QueuedConnection);

int main(int argc, char* argv[]) {
	const QGuiApplication app(argc, argv);
	QQmlApplicationEngine engine;
	const QUrl url("qrc:/qml/main.qml");

	SoundLooper looper;
	engine.rootContext()->setContextProperty("soundLooper", &looper);
	engine.load(url);

	return QGuiApplication::exec();
}
